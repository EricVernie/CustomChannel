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
using OnPremise.DirectLine.DataModel;
using System;
using System.Collections.Generic;

namespace OnPremise.DirectLine.Connector.Services
{
    public class ConnectorHelper
    {
        public static Conversation CreateNewConversation()
        {
            string cvId = Guid.NewGuid().ToString();
            //cvId = "123456";
            return new Conversation
            {
                ConversationId = cvId,
                Token = cvId,
                StreamUrl = "",
                ExpiresIn = new TimeSpan(1, 15, 0).TotalMinutes
            };
        }
        public static void AddingInfoToActivity(ref  Activity activity)
        {
          
        }
        private static Activity CreateActivity(ActiveBot currentBot, string channelUrl, bool addMember = true)
        {
            Activity activity = new Activity();
            activity.Timestamp = DateTime.UtcNow;
            activity.Conversation = new ConversationAccount(id: currentBot.Conversation.ConversationId);
            activity.Id = Guid.NewGuid().ToString();

            activity.ServiceUrl = channelUrl;
           // activity.From = new ChannelAccount(id: currentBot.UserById.Id, name: currentBot.UserById.Name);
            activity.LocalTimestamp = new DateTimeOffset(DateTime.UtcNow);

            activity.Recipient = new ChannelAccount(id: "12345MyBot", name: "Bot");

            activity.ChannelId = "emulator";
            if (addMember == true)
            {
                activity.MembersAdded = new List<ChannelAccount>();
                activity.MembersAdded.Add(activity.From);
            }
            //TODO: 
            activity.Locale = "en-Us";
            return activity;
        }
        public static string BuildChannelUrl(HttpRequest request)
        {
            return $"{request.Scheme}://{request.Host}";
        }

        public static Activity CreateConversationUpdateActivity(ActiveBot currentBot, string channelUri)
        {
            var activity = CreateActivity(currentBot, channelUri);
            activity.Type = "conversationUpdate";
            return activity;
        }

    }
}
