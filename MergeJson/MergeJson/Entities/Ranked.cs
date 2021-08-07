using System.Collections.Generic;
using Newtonsoft.Json;

namespace MergeJson.Entities
{
    public class Ranked
    {
        [JsonProperty("priority")]
        public int Priority { get; set; }

        [JsonProperty("vals")]
        public Dictionary<string, object> Vals { get; set; }
    }
}