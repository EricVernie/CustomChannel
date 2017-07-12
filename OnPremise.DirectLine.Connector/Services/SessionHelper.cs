
// ******************************************************************
// Copyright (c) Microsoft. All rights reserved.
// This code is licensed under the MIT License (MIT).
// THE CODE IS PROVIDED “AS IS”, WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED,
// INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
// IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM,
// DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT,
// TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH
// THE CODE OR THE USE OR OTHER DEALINGS IN THE CODE.
// ******************************************************************

using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using OnPremise.DirectLine.DataModel;
using System.Collections.Concurrent;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace OnPremise.DirectLine.Connector.Services
{
    //NAIVE IMPLEMENTATION DON'T USE IN PRODUCTION

    public static class SessionExtension 
    {
        private static ConcurrentDictionary<string, ISession> _session = null;
        public static ISession GetCurrentSession(this ISession session, string conversationId)
        {
            ISession outSession = null;
            if (_session !=null)
            {
                _session.TryGetValue(conversationId, out outSession);
            }
            
            return outSession;
        }
        public static BotData GetStateBotData(this ISession session, string key)
        {
            BotData botData = null;
            if (session !=null)
            {
                botData = GetData<BotData>(session, key);
            }
            
            return botData ?? new BotData { Data = null, ETag = "*" }; ;
        }
        public static void SaveStateBotData(this ISession session, string key, BotData botData)
        {
          
            SaveData<BotData>(session, key, botData);
        }

        public static void SaveConversationForCurrentUser(this ISession session, Conversation conversation)
        {
            var key = $"currentconversation_{conversation.ConversationId}";
            SaveData<Conversation>(session, key, conversation);            
        }
        public static Conversation GetConversationForCurrentUser(this ISession session, string conversationId)
        {
            var key = $"currentconversation_{conversationId}";
            Conversation conversation = GetData<Conversation>(session, key);
            return conversation;
        }
        public static void SaveConnectionId(this ISession session, string key, string connectionId)
        {
            session.SetString(key, connectionId);
        }
        public static string GetConnectionId(this ISession session, string key)
        {
            return session.GetString(key);
        }

        public static void InitSession(this ISession session, ActiveBot bot)
        {
            if (_session == null)
            {
                _session = new ConcurrentDictionary<string, ISession>();
            }
            _session.GetOrAdd(bot.Conversation.ConversationId, session);
            
        }

        public static void SaveSessionForCurrentUser(this ISession session, string conversationId,string userId)
        {
            ISession currentSession = session.GetCurrentSession(conversationId);
            _session.GetOrAdd(userId, currentSession);
        }

        public static void InitWatermark(this ISession session, string conversationId, int initwatermark = 0)
        {
           
            SaveCurrentNumberWatermark(session, $"Watermark_{conversationId}", initwatermark);
            MarkWatermark watermark = new MarkWatermark();

            watermark.Activities = new Activity[1];
            watermark.Activities[0] = new Activity();
            //watermark.Activities[1] = new Activity();
            watermark.Watermark = initwatermark;
            SaveWatermark(session, $"Watermark_{conversationId}_{initwatermark}", watermark);
        }
        public static void SaveWatermark(this ISession session, Activity activity, bool replyMessage)
        {
            SaveWatermark(session, activity.Conversation.Id, activity, replyMessage);
        }
        public static void SaveWatermark(this ISession session, string conversationId, Activity activity, bool replyMessage)
        {

            //TODO: Use a safe thread collection
            int currentNumberWatermark = GetCurrentNumberWatermark(session, $"Watermark_{conversationId}");
            if (currentNumberWatermark ==-1)
            {
                throw new HttpRequestException("Internal Server Error");
            }
            MarkWatermark currentWatermark = GetWatermark(session, conversationId, currentNumberWatermark);


            currentWatermark.Activities[0] = activity;
            
            currentWatermark.Watermark +=1;

            SaveWatermark(session, $"Watermark_{conversationId}_{currentNumberWatermark}", currentWatermark);
            currentNumberWatermark += 1;
            InitWatermark(session, conversationId, currentNumberWatermark);
        }
        private static void SaveWatermark(this ISession session,string key, MarkWatermark watermark)
        {
            SaveData<MarkWatermark>(session, key, watermark);
        }
        public static MarkWatermark GetWatermark(this ISession session, string conversationId, int watermarkId)
        {
            string key = $"Watermark_{conversationId}_{watermarkId}";
            return GetData<MarkWatermark>(session, key);
        }

        public static void SaveCurrentNumberWatermark(this ISession session, string key, int number)
        {

            session.SetInt32(key, number);
        }
        /// <summary>
        /// Return the current Watermark for a particular conversation
        /// </summary>
        /// <param name="session"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static int GetCurrentNumberWatermark(ISession session, string key)
        {
            var currentWatermark = session.GetInt32(key);
            if (currentWatermark.HasValue)
            {
                return currentWatermark.Value;
            }
            return -1;
        }
        private static T GetData<T>(ISession session, string key)
        {
            T t = default(T);
            if (session == null)
            {
                return t;
            }
            string data = session.GetString(key);
            
            if (data == null)
            {
                return t;
            }
            return JsonConvert.DeserializeObject<T>(data);
        }
        private static void SaveData<T>(ISession session, string key, T t)
        {
            string data = JsonConvert.SerializeObject(t);
            session.SetString(key, data);
        }

       
    }
    
}
