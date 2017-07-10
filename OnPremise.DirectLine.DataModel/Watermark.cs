using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnPremise.DirectLine.DataModel
{
    public class MarkWatermark
    {
        [JsonProperty("activities")]
        public Activity[] Activities { get; set; }
        [JsonProperty("watermark")]
        public int Watermark { get; set; }

    }

}
