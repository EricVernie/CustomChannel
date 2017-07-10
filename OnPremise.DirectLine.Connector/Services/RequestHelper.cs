
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
using System.Text;
using System.Threading.Tasks;

namespace OnPremise.DirectLine.Connector.Services
{
    public static class RequestHelper
    {
        public static bool IsMethodOptions(this HttpRequest request)
        {
            return request.Method.Equals("OPTIONS") ? true : false;
        }
        public static string GetConversationId(this HttpRequest request)
        {
            string[] splitedPath=null;
            if (request.Path.HasValue)
            {
                splitedPath = request.Path.Value.Split('/');
            }
            
            return splitedPath[4];
        }
        public static async Task<Activity> GetActivityAsync(this HttpRequest request)
        {
            string data = await  request.GetStringAsync();

            return JsonConvert.DeserializeObject<Activity>(data);
        }
        public static async Task<string> GetStringAsync(this HttpRequest request)
        {
            //TODO: Potential bug here
            int length = (int)request.ContentLength;
            if (length == 0) return null;

            byte[] buffer = new byte[length];
            await request.Body.ReadAsync(buffer, 0, length);
            return Encoding.UTF8.GetString(buffer);
        }
    }
}
