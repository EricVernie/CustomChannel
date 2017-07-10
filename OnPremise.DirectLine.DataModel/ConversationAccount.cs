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
    /// <summary>
    /// Channel account information for a conversation
    /// </summary>
    public partial class ConversationAccount
    {
        /// <summary>
        /// Initializes a new instance of the ConversationAccount class.
        /// </summary>
        public ConversationAccount() { }

        /// <summary>
        /// Initializes a new instance of the ConversationAccount class.
        /// </summary>
        public ConversationAccount(bool? isGroup = default(bool?), string id = default(string), string name = default(string))
        {
            IsGroup = isGroup;
            Id = id;
            Name = name;
        }

        /// <summary>
        /// Is this a reference to a group
        /// </summary>
        [JsonProperty(PropertyName = "isGroup")]
        public bool? IsGroup { get; set; }

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
