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
using OnPremise.DirectLine.DataModel;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OnPremise.DirectLine.Connector.Services
{

    public class AuthenticationHelper
    {
        
        //PUT in cache instead
        private static string _accessToken;
        public static async Task<string> GetAccessTokenAsync(string msaAppId, string msaPassword)
        {
            try
            {
                if (string.IsNullOrEmpty(_accessToken))
                {
                    HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "https://login.microsoftonline.com/common/oauth2/v2.0/token")
                    {
                        Content = new StringContent($"grant_type=client_credentials&client_id={msaAppId}&client_secret={msaPassword}&scope=https%3A%2F%2Fgraph.microsoft.com%2F.default", Encoding.UTF8, "application/x-www-form-urlencoded")
                    };
                    var response = await HttpHelper.Instance.SendAsync(request, CancellationToken.None).ConfigureAwait(false);
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        string rawAccessToken = await response.Content.ReadAsStringAsync();
                       
                        var jwtToken = JsonConvert.DeserializeObject<JWToken>(rawAccessToken);
                        _accessToken = jwtToken.AccessToken;
                    }
                    else
                    {
                        throw new HttpRequestException("Unauthorized request");
                    }


                }
            }
            catch (Exception ex)
            {
                var a = ex.Message;
            }

            //TODO: Redeem token when expire

            return _accessToken;
        }

        public static async Task<string> GetJwtTokenAsync(string msaAppId, string msaPassword)
        {
            HttpHelper.InitHttpClient();
            try
            {
                if (string.IsNullOrEmpty(_accessToken))
                {
                    HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "https://login.microsoftonline.com/common/oauth2/v2.0/token")
                    {
                        Content = new StringContent($"grant_type=client_credentials&client_id={msaAppId}&client_secret={msaPassword}&scope=https%3A%2F%2Fgraph.microsoft.com%2F.default", Encoding.UTF8, "application/x-www-form-urlencoded")
                    };
                    var response = await HttpHelper.Instance.SendAsync(request, CancellationToken.None).ConfigureAwait(false);
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        return await response.Content.ReadAsStringAsync();

                        
                    }
                    else
                    {
                        throw new HttpRequestException("Unauthorized request");
                    }


                }
            }
            catch (Exception ex)
            {
                var a = ex.Message;
            }

            //TODO: Redeem token when expire

            return _accessToken;
        }
    }
}
