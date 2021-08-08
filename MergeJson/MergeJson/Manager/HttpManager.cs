using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace MergeJson.Manager
{
    public class HttpManager : IHttpManager
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger _logger;

        public HttpManager(HttpClient httpClient, ILogger<HttpManager> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<string> GetMergedJson()
        {
            _logger.LogInformation($"Sending GET request to the host {_httpClient.BaseAddress}");
            try
            {
                var httpResponse = await _httpClient.GetAsync(string.Empty);
                var data = await httpResponse.Content.ReadAsStringAsync();
                _logger.LogInformation($"Data received: {data}");
                return data;
            }
            catch (HttpRequestException e)
            {
                _logger.LogError($"Cannot get data from the host. {e.Message}");
                throw;
            }
        }
    }
}
