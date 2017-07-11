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
using OnPremise.DirectLine.Connector.Services;
using OnPremise.DirectLine.DataModel;
using System;

namespace OnPremise.DirectLine.Connector.IISExpress.Controllers
{
    [Produces("application/json")]
    [Route("v3/conversations")]    
    public class ReplyActivityController : Controller
    {
        [HttpOptions]
        [Route("{conversationId}/activities/{activityId}")]
        public ActionResult AreYouAlive()
        {
            return this.Ok();
        }
        [HttpPost]
        [Route("{conversationId}/activities/{activityId}")]
        public ResourceResponse ReplyToActivityAsync([FromBody]Activity activity,string conversationId)
        {


            ResourceResponse resourceResponse = new ResourceResponse(activity.ReplyToId);

            activity.Id = Guid.NewGuid().ToString();
            this.HttpContext.Session.GetCurrentSession(conversationId).SaveWatermark(activity, true);
            

            //Reply to client 
            //NotificationService service = new NotificationService();
            //Conversation currentConversation= SessionHelper.GetConversataion($"currentconversation_{activity.Conversation.Id}");
            //ExceptionHelper.CheckAndThrow<Conversation>(currentConversation);

            //if (activity.Conversation.Id == currentConversation.conversationId)
            //{
            //service.ReplyToClient(activity);
            //}

            return resourceResponse;

        }

    }
}