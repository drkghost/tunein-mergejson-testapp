using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Memory;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Net.Http;
using MergeJson.Algorithm;

namespace MergeJson
{
    internal class Program
    {
        public static IConfigurationRoot configuration;

        static void Main(string[] args)
        {
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            var serviceProvider = serviceCollection.BuildServiceProvider();

            var json =
                "{\"ranked\":[{\"priority\":2,\"vals\":{\"timeout\":\"3s\",\"num_threads\":500,\"buffer_size\":4000,\"use_sleep\":true}}," +
                "{\"priority\":1,\"vals\":{\"timeout\":\"2s\",\"startup_delay\":\"2m\",\"skip_percent_active\":0.2}}," +
                "{\"priority\":0,\"vals\":{\"num_threads\":300,\"buffer_size\":3000,\"label\":\"testing\"}}]}";


            var mergeJson = serviceProvider.GetService<IMergeAlgorithm>();
            mergeJson.Merge(json);
        }

        private static void ConfigureServices(IServiceCollection serviceCollection)
        {
            configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetParent(AppContext.BaseDirectory).FullName)
                .AddJsonFile("appsettings.json", false)
                .Build();
            serviceCollection
                .AddSingleton<IConfigurationRoot>(configuration)
                .AddSingleton<IMergeAlgorithm, MergeAlgorithm>();

            var httpClient = new HttpClient() { BaseAddress = new Uri(configuration.GetSection("BaseAddress").Value) };
            serviceCollection.AddSingleton<HttpClient>(httpClient);
        }
    }
}
