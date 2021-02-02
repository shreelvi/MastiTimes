using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MastiTimes.Models
{
    /// <summary>
    /// Class to map results for recent news 
    /// </summary>
    
    [JsonConverter(typeof(StringEnumConverter))]
    public class Articles
    {
        [JsonProperty("id")]
        public string ID { get; set; }

        [JsonProperty("title")]
        public string title { get; set; }

        [JsonProperty("description")]
        public string description { get; set; }

        [JsonProperty("url")]
        public string url { get; set; }

        [JsonProperty("urlToImage")]
        public string urlToImage { get; set; }

        [JsonProperty("publishedAt")]
        public string publishedAt { get; set; }

        [JsonProperty("content")]
        public string content { get; set; }



    }
}
