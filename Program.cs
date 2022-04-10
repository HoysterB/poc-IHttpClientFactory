using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Net.Http.Headers;

namespace poc_IHttpClientFactory
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHttpClient<Worker>(nameof(Worker), opt =>
                    {
                        opt.BaseAddress = new Uri("https://api.binance.com/");
                    });

                    services.AddHostedService<Worker>();
                });
    }
}
