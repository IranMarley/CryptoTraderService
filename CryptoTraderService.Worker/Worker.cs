using CryptoTraderService.Worker.Entities;
using CryptoTraderService.Worker.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RestSharp;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CryptoTraderService.Worker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger _logger;
        private readonly ITrade _trade;
        private readonly WorkerSettings _workerSettings;

        public Worker
        (
            ILogger<Worker> logger,
            ITrade trade,
            IConfiguration configuration
        )
        {
            _logger = logger;
            _trade = trade;
            _workerSettings = configuration
                .GetSection(typeof(WorkerSettings).Name).Get<WorkerSettings>();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation($"Worker execution started at: {DateTimeOffset.Now}");

                await _trade.Execute();

                _logger.LogInformation($"Worker execution finished at: {DateTimeOffset.Now}");

                await Task.Delay(_workerSettings.IntervalInMilliseconds, stoppingToken);
            }
        }
    }
}

