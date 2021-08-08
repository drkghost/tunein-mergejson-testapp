using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Net.Http;
using MergeJson.Algorithm;
using MergeJson.Extensions;
using MergeJson.Manager;

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

            var manager = serviceProvider.GetService<IHttpManager>();
            var mergeJson = serviceProvider.GetService<IMergeAlgorithm>();

            var json = manager.GetMergedJson().Result;
            var result = mergeJson.Merge(json);
            Console.WriteLine(result.ToDebugString());
        }

        private static void ConfigureServices(IServiceCollection serviceCollection)
        {
            var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetParent(AppContext.BaseDirectory).FullName)
                .AddJsonFile("appsettings.json", true, true)
                .AddJsonFile($"appsettings.{environmentName}.json", true, true)
                .Build();
            serviceCollection
                .AddSingleton<IConfigurationRoot>(configuration)
                .AddSingleton<IMergeAlgorithm, MergeAlgorithm>()
                .AddSingleton<IHttpManager, HttpManager>();
            
            var httpClient = new HttpClient() { BaseAddress = new Uri(configuration.GetSection("BaseAddress").Value) };
            serviceCollection.AddSingleton<HttpClient>(httpClient);
        }
    }
}
