using System.Collections.Generic;
using System.Linq;

namespace MergeJson.Extensions
{
    public static class DictionaryExtensions
    {
        public static string ToDebugString<TKey, TValue>(this IDictionary<TKey, TValue> dictionary)
        {
            return "\r\n{\r\n" + string.Join(",\r\n", dictionary.Select(kv => "  " + kv.Key + "=" + kv.Value).ToArray()) + "\r\n}\r\n";
        }
    }
}