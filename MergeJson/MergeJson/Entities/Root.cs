using System.Collections.Generic;
using Newtonsoft.Json;

namespace MergeJson.Entities
{
    public class Root
    {
        [JsonProperty("ranked")]
        public List<Ranked> Ranked { get; set; }
    }
}