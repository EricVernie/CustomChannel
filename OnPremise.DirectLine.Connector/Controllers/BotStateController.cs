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
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnPremise.DirectLine.Connector.Services;
using OnPremise.DirectLine.DataModel;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace OnPremise.DirectLine.Connector.IISExpress.Controllers
{
    [Produces("application/json")]
    [Route("v3/botstate/emulator")]
    
    public class BotStateController : Controller
    {
        
        [HttpGet]
        [Route("users/{userId}")]
        public BotData GetUserData(string userId)
        {
            
            return this.HttpContext.Session.GetCurrentSession(userId).GetStateBotData($"user_{userId}");
            
        }
        [HttpGet]
        [Route("conversations/{conversationId}/users/{userId}")]
        public BotData GetPrivateConversationData(string conversationId,  string userId)
        {
            
            return HttpContext.Session.GetCurrentSession(conversationId).GetStateBotData($"privateconversation_{conversationId}_user_{userId}");
        }

        [HttpGet]
        [Route("conversations/{conversationId}")]
        public BotData GetConversationData(string conversationId)
        {
            
            return HttpContext.Session.GetCurrentSession(conversationId).GetStateBotData($"conversation_{conversationId}");
        }

        [HttpPost]
        [Route("conversations/{conversationId}")]
        public IActionResult SetConversationData([FromBody]BotData botData,string conversationId)
        {
            
            this.HttpContext.Session.GetCurrentSession(conversationId).SaveStateBotData($"conversation_{conversationId}", botData);

            return this.Ok();
        }
        [HttpPost]
        [Route("conversations/{conversationId}/users/{userId}")]
        public IActionResult SetPrivateConversationData([FromBody]BotData botData,string conversationId, string userId)
        {
            
            this.HttpContext.Session.GetCurrentSession(conversationId).SaveStateBotData($"privateconversation_{conversationId}_user_{userId}", botData);
            return this.Ok();
        }
        [HttpPost]
        [Route("users/{userId}")]
        public IActionResult SetUserData([FromBody]BotData botData, string userId)
        {
            
            this.HttpContext.Session.GetCurrentSession(userId).SaveStateBotData($"user_{userId}", botData);
            return this.Ok();
        }
        
    }
}