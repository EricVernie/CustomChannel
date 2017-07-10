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
using OnPremise.DirectLine.DataModel;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace OnPremise.DirectLine.Connector.Client
{
    public class ClientHelper
    {
        Transport _transport;
        string _channelUrl;
        string _routeApi = "v3/directline";
        public ClientHelper(string channelUrl)
        {
            _transport = new Transport(channelUrl);
            _channelUrl = channelUrl;
        }
        private HttpRequestMessage CreateRequest(HttpMethod method, string uri)
        {
            return new HttpRequestMessage(method, $"{_channelUrl}/{_routeApi}/{uri}");
        }
        public async Task<Conversation> StartNewConversationAsync()
        {

            return await _transport.SendAsync<Conversation>(CreateRequest(HttpMethod.Post,"conversations"));
        }

        public async Task<Me> GetAuthor()
        {            
            return await _transport.SendAsync<Me>(CreateRequest(HttpMethod.Get, "author"));
        }

        /// <summary>
        /// Get JWTToken
        /// </summary>
        /// <returns></returns>
        public async Task<JWToken> GetTokenAsync()
        {
            
            //TODO: need to pass a secret for security reason
            
            return await _transport.SendAsync<JWToken>(CreateRequest(HttpMethod.Post,"tokens/generate"));
        }
    }
}
