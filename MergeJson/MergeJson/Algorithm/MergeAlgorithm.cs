using System.Collections.Generic;
using System.Linq;
using MergeJson.Entities;
using MergeJson.Extensions;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace MergeJson.Algorithm
{
    public class MergeAlgorithm : IMergeAlgorithm
    {
        private readonly ILogger _logger;

        public MergeAlgorithm(ILogger<MergeAlgorithm> logger)
        {
            _logger = logger;
        }

        public Dictionary<string, object> Merge(string serializedJson)
        {
            _logger.LogInformation($"Processing serialized json object: {serializedJson}");
            if (string.IsNullOrEmpty(serializedJson))
            {
                return null;
            }

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
            _logger.LogInformation($"Result: {result.ToDebugString()}");

            return result;
        }
    }
}
