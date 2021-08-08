using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Net.Http;
using MergeJson.Algorithm;
using MergeJson.Manager;
using Microsoft.Extensions.Logging;

namespace MergeJson
{
    internal class Program
    {
        private static IConfigurationRoot _configuration;

        static void Main(string[] args)
        {
            // configure di
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            var serviceProvider = serviceCollection.BuildServiceProvider();

            // get all requires services
            var manager = serviceProvider.GetService<IHttpManager>();
            var mergeJson = serviceProvider.GetService<IMergeAlgorithm>();

            // request data from api
            var json = manager.GetMergedJson().Result;
            mergeJson.Merge(json);
        }

        private static void ConfigureServices(IServiceCollection serviceCollection)
        {
            var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            _configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetParent(AppContext.BaseDirectory).FullName)
                .AddJsonFile("appsettings.json", true, true)
                .AddJsonFile($"appsettings.{environmentName}.json", true, true)
                .Build();
            serviceCollection
                .AddSingleton<IConfigurationRoot>(_configuration)
                .AddSingleton<IMergeAlgorithm, MergeAlgorithm>()
                .AddSingleton<IHttpManager, HttpManager>()
                .AddLogging(config => config.AddConsole())
                .AddSingleton<HttpClient>(new HttpClient() { BaseAddress = new Uri(_configuration.GetSection("BaseAddress").Value) });
        }
    }
}
