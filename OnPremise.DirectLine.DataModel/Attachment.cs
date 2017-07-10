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
    /// An attachment within an activity
    /// </summary>
    public class Attachment
    {
        /// <summary>
        /// Initializes a new instance of the Attachment class.
        /// </summary>
        public Attachment() { }

        /// <summary>
        /// Initializes a new instance of the Attachment class.
        /// </summary>
        public Attachment(string contentType = default(string), string contentUrl = default(string), object content = default(object), string name = default(string), string thumbnailUrl = default(string))
        {
            ContentType = contentType;
            ContentUrl = contentUrl;
            Content = content;
            Name = name;
            ThumbnailUrl = thumbnailUrl;
        }

        /// <summary>
        /// mimetype/Contenttype for the file
        /// </summary>
        [JsonProperty(PropertyName = "contentType")]
        public string ContentType { get; set; }

        /// <summary>
        /// Content Url
        /// </summary>
        [JsonProperty(PropertyName = "contentUrl")]
        public string ContentUrl { get; set; }

        /// <summary>
        /// Embedded content
        /// </summary>
        [JsonProperty(PropertyName = "content")]
        public object Content { get; set; }

        /// <summary>
        /// (OPTIONAL) The name of the attachment
        /// </summary>
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        /// <summary>
        /// (OPTIONAL) Thumbnail associated with attachment
        /// </summary>
        [JsonProperty(PropertyName = "thumbnailUrl")]
        public string ThumbnailUrl { get; set; }

    }
}
