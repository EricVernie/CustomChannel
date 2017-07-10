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

using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OnPremise.DirectLine.Connector.Services;
using OnPremise.DirectLine.DataModel;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace OnPremise.DirectLine.Connector.Controllers
{
    [Produces("application/json")]    
    [Route("v3/directline")]
    public class ConversationsController : Controller
    {
        [HttpGet]
        [Route("author")]
        public string GetAuthor()
        {            
            return "{'author':'(c) Eric Vernié','date' :'July 2017'}";
        }
       

        [HttpPost]
        [Route("conversations")]
        public async Task<IActionResult> StartWebChatConversationAsync()
        {
           
            string botUrl = ConfigurationHelper.GetOption("Bot");
            ActiveBot currentBot = ActiveBot.CreateActiveBot(botUrl); 
            
            return await this.StartNewConversationAsync(currentBot);
            
        }

        //This is only for a desktop custom client
        [HttpPost]
        [Route("desktopemulator/v3/directline/conversations")]
        public async Task<IActionResult> StartNewConversationAsync([FromBody]ActiveBot activeBot)
        {

            
            activeBot.Conversation = ConnectorHelper.CreateNewConversation();
            
            var activity = ConnectorHelper.CreateConversationUpdateActivity(activeBot, ConnectorHelper.BuildChannelUrl(this.Request));
            this.HttpContext.Session.InitSession(activeBot);
            this.HttpContext.Session.InitWatermark(activeBot.Conversation.ConversationId);
            this.HttpContext.Session.SaveConversationForCurrentUser(activeBot.Conversation);
            HttpHelper.InitHttpClient(this.Request);
            await HttpHelper.PostActivityAsync(activity, this.Request, activeBot.ServiceUrl);
            return this.Created(activity.ServiceUrl, activeBot.Conversation);
            
        }

        [HttpPost]
        [Route("conversations/{conversationId}/activities")]
        public async Task<ActivityId> SendMessageAsync([FromBody] Activity activity,string conversationId)
        {

            activity.Id = Guid.NewGuid().ToString();
            activity.ServiceUrl = ConnectorHelper.BuildChannelUrl(this.Request);
            activity.ChannelId = "emulator";
            activity.Recipient = new ChannelAccount(id: "12345MyBot", name: "Bot");
            activity.Conversation = new ConversationAccount { Id = conversationId };

            var accessToken=this.Request.Headers["Authorization"];
            this.HttpContext.Session.SaveSessionForCurrentUser(conversationId, activity.From.Id);
            string botUrl = ConfigurationHelper.GetOption("Bot");
            await HttpHelper.PostActivityAsync(activity, this.Request, botUrl);
            ActivityId activityId = new ActivityId { Id = activity.Id };
            
            return activityId;
        }

       
        //This method is call (Polling) by the WebChat control
        [HttpGet("{watermark}")]
        [Route("conversations/{conversationId}/activities")]
        public IActionResult GetWatermark([FromQuery] int? watermark, string conversationId)
        {
                        
            int watermarkId = 0;
            if (watermark.HasValue)
            {
                watermarkId = watermark.Value;
            }
            var saveWatermark = this.HttpContext.Session.GetCurrentSession(conversationId).GetWatermark(conversationId, watermarkId);
            
            return this.Ok(saveWatermark);
        }
    }
}