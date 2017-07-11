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
using System.Threading.Tasks;

namespace OnPremise.DirectLine.Connector.IISExpress.Controllers
{
    [Produces("application/json")]
   
    public class TokenController : Controller
    {

        [HttpPost]
        [Route("v3/directline/tokens/generate")]
        public async Task<string> GenerateToken()
        {
            //TODO: Get The token here :-)

            //TODO: put in config
            var MicrosoftAppId = "28141eb5-4828-4eff-8210-f58c53a8dfe4";
            var MicrosoftAppPassword = "khzA1WykDWUb6gOXpuQ7GtV";
            return await AuthenticationHelper.GetJwtTokenAsync(MicrosoftAppId, MicrosoftAppPassword);

        }
    }
}