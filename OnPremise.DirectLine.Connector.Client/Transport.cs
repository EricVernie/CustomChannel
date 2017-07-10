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
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace OnPremise.DirectLine.Connector.Client
{
   
        public static class HttpResponseMessageExtension
        {
            public async static Task<T> DeserializeAsync<T>(this HttpResponseMessage response)
            {

                string JsonContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                return JsonConvert.DeserializeObject<T>(JsonContent);
                // 
            }
        }
        public class Transport
        {
            private static HttpClient _httpProvider;

            public HttpClient HttpProvider { get { return _httpProvider; } set { _httpProvider = value; } }
            public Transport(string baseUrl)
            {
                if (_httpProvider == null)
                {
                    _httpProvider = new HttpClient();
                    _httpProvider.BaseAddress = new Uri(baseUrl);
                }
            }
            public async Task<T> SendAsync<T>(HttpRequestMessage request, string accessToken = null, CancellationToken token = default(CancellationToken))
            {
                if (accessToken != null)
                {
                    _httpProvider.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                }

                T t = default(T);
                using (HttpResponseMessage Response = await _httpProvider.SendAsync(request, token).ConfigureAwait(false))
                {

                    if (Response.StatusCode == HttpStatusCode.OK)
                    {

                        t = await Response.DeserializeAsync<T>().ConfigureAwait(false);

                    }
                    else
                    {
                        throw new HttpRequestException(Response.ReasonPhrase);
                    }

                }
                return t;
            }
        }
    }
