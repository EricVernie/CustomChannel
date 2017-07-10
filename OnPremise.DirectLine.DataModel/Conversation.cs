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
    public class Conversation
    {
        [JsonProperty("conversationId")]
        public string ConversationId { get; set; }
        [JsonProperty("token")]
        public string Token { get; set; }
        [JsonProperty("expires_in")]
        public double ExpiresIn { get; set; }
        [JsonProperty("streamUrl")]
        public string StreamUrl { get; set; }    
        public string Serialize()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
