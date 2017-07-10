using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnPremise.DirectLine.DataModel
{

    public class Me
    {
        [JsonProperty("author")]
        public string Author { get; set; }
        [JsonProperty("date")]
        public string Date { get; set; }
    }
    

}
