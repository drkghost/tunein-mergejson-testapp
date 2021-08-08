using System.Net.Http;
using System.Threading.Tasks;

namespace MergeJson.Manager
{
    public class HttpManager : IHttpManager
    {
        private readonly HttpClient _httpClient;
        
        public HttpManager(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> GetMergedJson()
        {
            var httpResponse = await _httpClient.GetAsync(string.Empty);
            var data = await httpResponse.Content.ReadAsStringAsync();
            return data;
        }
    }
}
