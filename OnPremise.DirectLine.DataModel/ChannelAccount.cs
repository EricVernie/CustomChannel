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
using System.Collections.Generic;
using System.Text;

namespace OnPremise.DirectLine.DataModel
{
    public partial class ChannelAccount
    {
        /// <summary>
        /// Initializes a new instance of the ChannelAccount class.
        /// </summary>
        public ChannelAccount() { }

        /// <summary>
        /// Initializes a new instance of the ChannelAccount class.
        /// </summary>
        public ChannelAccount(string id = default(string), string name = default(string))
        {
            Id = id;
            Name = name;
        }

        /// <summary>
        /// Channel id for the user or bot on this channel (Example:
        /// joe@smith.com, or @joesmith or 123456)
        /// </summary>
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        /// <summary>
        /// Display friendly name
        /// </summary>
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

    }
}
