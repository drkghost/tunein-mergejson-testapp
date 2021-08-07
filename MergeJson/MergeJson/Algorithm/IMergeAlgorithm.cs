using System.Collections.Generic;

namespace MergeJson.Algorithm
{
    public interface IMergeAlgorithm
    {
        public Dictionary<string, object> Merge(string serializedJson);
    }
}