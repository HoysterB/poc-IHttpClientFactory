using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace poc_IHttpClientFactory
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly HttpClient _httpClient;
        public Worker(ILogger<Worker> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClient = httpClientFactory.CreateClient(nameof(Worker));
        }

        protected override async Task ExecuteAsync(
                CancellationToken stoppingToken
            )
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

                var response = await _httpClient.GetAsync("api/v3/ticker/price");

                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();

                    _logger.LogInformation(await response.Content.ReadAsStringAsync());
                }
                else
                {
                    _logger.LogInformation($"ERROR CODE: {response.StatusCode}");
                }

                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
