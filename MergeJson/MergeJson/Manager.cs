using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace MergeJson
{
    public class Manager
    {
        private readonly HttpClient _httpClient;
        private readonly IConfigurationRoot _config;

        public Manager(HttpClient httpClient, IConfigurationRoot config)
        {
            _httpClient = httpClient;
            _config = config;
        }

        public async Task<string> GetMergedJson()
        {
            var httpResponse = await _httpClient.GetAsync(string.Empty);
            var data = await httpResponse.Content.ReadAsStringAsync();
            return data;
        }
    }
}
