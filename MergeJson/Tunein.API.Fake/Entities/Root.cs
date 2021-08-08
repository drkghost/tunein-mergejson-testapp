using System.Collections.Generic;
using Newtonsoft.Json;

namespace Tunein.API.Fake.Entities
{
    public class Root
    {
        [JsonProperty("ranked")]
        public List<Ranked> Ranked { get; set; }
    }
}