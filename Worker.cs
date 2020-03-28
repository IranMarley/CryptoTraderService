using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using CryptoTraderService.Entities;
using CryptoTraderService.Services;
using RestSharp;
using System.Linq;

namespace CryptoTraderService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly ServiceConfigurations _serviceConfigurations;

        public Worker(ILogger<Worker> logger,
           IConfiguration configuration)
        {
            _logger = logger;

            _serviceConfigurations = new ServiceConfigurations();
            new ConfigureFromConfigurationOptions<ServiceConfigurations>(
                configuration.GetSection("ServiceConfigurations"))
                    .Configure(_serviceConfigurations);
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker executed at: {time}", DateTimeOffset.Now);

                var host = _serviceConfigurations.Host;
                var token = _serviceConfigurations.Token;
                var limitAmount = _serviceConfigurations.LimitAmount;
                var minValue = _serviceConfigurations.MinValue;
                var maxValue = _serviceConfigurations.MaxValue;

                try
                {
                    #region Orders

                    var responseOrdersWaiting = new Request().SendRequestMarket($"{host}/v2/market/user_orders/list?status=waiting&start_date=&end_date=&pair=BRLETH&type=buy&page_size=1&current_page=1", token, null, Method.GET);
                    var ordersWating = JsonConvert.DeserializeObject<UserOrder>(responseOrdersWaiting);

                    #endregion


                    #region Balance

                    var responseBalance = new Request().SendRequest($"{host}/v3/wallets/balance", token, null, Method.GET);
                    var balance = JsonConvert.DeserializeObject<Balance>(responseBalance);

                    #endregion

                    var brl = balance.data.First(f => f.currency_code == "BRL");
                    var eth = balance.data.First(f => f.currency_code == "ETH");

                    var amount = brl.available_amount - limitAmount;

                    var responseEstimatedPrice = new Request().SendRequestMarket($"{host}/v2/market/estimated_price?amount=1&pair=BRLETH&type=buy", token, null, Method.GET);
                    var estimatedPrice = JsonConvert.DeserializeObject<EstimatedPrice>(responseEstimatedPrice).data.price;

                    if (brl.available_amount > limitAmount && brl.locked_amount == 0 && estimatedPrice < minValue)
                    {
                        var entity = new Order
                        {
                            pair = "BRLETH",
                            type = "buy",
                            subtype = "limited",
                            amount = (float)(amount / estimatedPrice),
                            unit_price = estimatedPrice + (float)0.0000001,
                            request_price = amount
                        };

                        var json = JsonConvert.SerializeObject(entity);
                        var response = new Request().SendRequest($"{host}/v3/market/create_order", token, json, Method.POST);

                        _logger.LogInformation(response);
                    }

                    else if (eth.available_amount > 0 && eth.locked_amount == 0 && estimatedPrice > maxValue)
                    {
                        var entity = new Order
                        {
                            pair = "BRLETH",
                            type = "sell",
                            subtype = "limited",
                            amount = (float)(eth.available_amount - 0.0000001),
                            unit_price = estimatedPrice
                        };

                        var json = JsonConvert.SerializeObject(entity);
                        var response = new Request().SendRequest($"{host}/v3/market/create_order", token, json, Method.POST);

                        _logger.LogInformation(response);
                    }

                }
                catch (Exception e)
                {
                    _logger.LogError(e.Message);
                }

                await Task.Delay(_serviceConfigurations.Interval, stoppingToken);
            }
        }
    }
}

