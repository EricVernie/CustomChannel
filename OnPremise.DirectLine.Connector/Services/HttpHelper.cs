
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
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OnPremise.DirectLine.Connector.Services
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
    public class HttpHelper
    {
        private static HttpClient _instance;
        // public static HttpClient Instance => _instance ?? (_instance = new HttpClient());
        public static HttpClient Instance { get { return _instance; } }
        public static void InitHttpClientWithAuthorizationHeader(HttpRequest context)
        {
             if (_instance == null)
            {
                _instance = new HttpClient();
                var accessTokenResult = GetTokenFromHeader(context);
                _instance.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(accessTokenResult.Item1, accessTokenResult.Item2);                
            }
        }
        public static void InitHttpClient()
        {
            if (_instance == null)
            {
                _instance = new HttpClient();
               
            }
        }

        private static System.Tuple<string, string> GetTokenFromHeader(HttpRequest context)
        {
            string accessTokenType = null;
            string accessToken = null;
            var accessTokenResult = context.Headers["Authorization"];
            if (accessTokenResult.Count > 0)
            {
                string[] splitedAccessToken = accessTokenResult[0].Split(' ');
                accessTokenType = splitedAccessToken[0];
                accessToken = splitedAccessToken[1];
            }
            return new System.Tuple<string, string>(accessTokenType, accessToken);
        }
      
        public static async Task PostActivityAsync(Activity activity, HttpRequest context, string serviceUrl)
        {
            //TODO: Authentificatoin

            //if (accessToken == null)
            //{
            //    //TRY TO AUTHENTICATE 
            //accessToken = await AuthenticationHelper.GetAccessTokenAsync(currentBot.msaAppId, currentBot.msaPassword);

            //}
            //if (accessToken == null)
            //{
            //   // throw new HttpRequestException("Unauthorized request");
            //}
          

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, serviceUrl);
            var content = JsonConvert.SerializeObject(activity);
            request.Content = new StringContent(content, Encoding.UTF8, "application/json");            
            var response = await _instance.SendAsync(request).ConfigureAwait(false);
            if (response.IsSuccessStatusCode == false)
            {
                throw new HttpRequestException(response.ReasonPhrase);
            }
         
        }
        public static async Task<T> SendAsync<T>(HttpRequestMessage request, CancellationToken token = default(CancellationToken))
        {

            T t = default(T);
            using (HttpResponseMessage Response = await _instance.SendAsync(request, token).ConfigureAwait(false))
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
