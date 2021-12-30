using Autofac;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using StockData.Worker.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace StockData.Worker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly DataScraperModel _dataScraperModel;

        public Worker(ILogger<Worker> logger, DataScraperModel dataScraperModel)
        {
            _logger = logger;
            _dataScraperModel = dataScraperModel;
        }

        public static ILifetimeScope AutofacContainer { get; set; }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

                if (_dataScraperModel.GetMarketStatus() == "Open")
                {
                    _dataScraperModel.StartProcessing();
                }
                else
                {
                    _logger.LogInformation("Market Status : Closed at: {time}", DateTimeOffset.Now);
                }                    
                await Task.Delay(60000, stoppingToken);
            }
        }
    }
}
