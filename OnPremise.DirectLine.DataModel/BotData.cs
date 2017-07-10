using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnPremise.DirectLine.DataModel
{
    public class BotData
    {
        /// <summary>
        /// Initializes a new instance of the BotData class.
        /// </summary>
        public BotData() { }

        /// <summary>
        /// Initializes a new instance of the BotData class.
        /// </summary>
        public BotData(string eTag = default(string), object data = default(object))
        {
            ETag = eTag;
            Data = data;
        }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "eTag")]
        public string ETag { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "data")]
        public object Data { get; set; }

    }
}
