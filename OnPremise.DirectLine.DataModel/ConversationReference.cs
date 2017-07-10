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

using Newtonsoft.Json;

namespace OnPremise.DirectLine.DataModel
{
    public partial class ConversationReference
    {
        /// <summary>
        /// Initializes a new instance of the ConversationReference class.
        /// </summary>
        public ConversationReference() { }

        /// <summary>
        /// Initializes a new instance of the ConversationReference class.
        /// </summary>
        public ConversationReference(string activityId = default(string), ChannelAccount user = default(ChannelAccount), ChannelAccount bot = default(ChannelAccount), ConversationAccount conversation = default(ConversationAccount), string channelId = default(string), string serviceUrl = default(string))
        {
            ActivityId = activityId;
            User = user;
            Bot = bot;
            Conversation = conversation;
            ChannelId = channelId;
            ServiceUrl = serviceUrl;
        }

        /// <summary>
        /// (Optional) ID of the activity to refer to
        /// </summary>
        [JsonProperty(PropertyName = "activityId")]
        public string ActivityId { get; set; }

        /// <summary>
        /// (Optional) User participating in this conversation
        /// </summary>
        [JsonProperty(PropertyName = "user")]
        public ChannelAccount User { get; set; }

        /// <summary>
        /// Bot participating in this conversation
        /// </summary>
        [JsonProperty(PropertyName = "bot")]
        public ChannelAccount Bot { get; set; }

        /// <summary>
        /// Conversation reference
        /// </summary>
        [JsonProperty(PropertyName = "conversation")]
        public ConversationAccount Conversation { get; set; }

        /// <summary>
        /// Channel ID
        /// </summary>
        [JsonProperty(PropertyName = "channelId")]
        public string ChannelId { get; set; }

        /// <summary>
        /// Service endpoint where operations concerning the referenced
        /// conversation may be performed
        /// </summary>
        [JsonProperty(PropertyName = "serviceUrl")]
        public string ServiceUrl { get; set; }

    }
}
