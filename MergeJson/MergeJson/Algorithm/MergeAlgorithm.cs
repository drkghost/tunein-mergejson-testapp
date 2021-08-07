using System.Collections.Generic;
using System.Linq;
using MergeJson.Entities;
using Newtonsoft.Json;

namespace MergeJson.Algorithm
{
    public class MergeAlgorithm : IMergeAlgorithm
    {
        public Dictionary<string, object> Merge(string serializedJson)
        {
            var data = JsonConvert.DeserializeObject<Root>(serializedJson);
            var orderedData = data.Ranked.OrderBy(x => x.Priority);
            var result = new Dictionary<string, object>();
            foreach (var item in orderedData)
            {
                foreach (var pair in item.Vals)
                {
                    if (result.GetValueOrDefault(pair.Key) == null && !pair.Key.StartsWith("skip"))
                    {
                        result.Add(pair.Key, pair.Value);
                    }
                }
            }

            return result;
        }
    }
}
