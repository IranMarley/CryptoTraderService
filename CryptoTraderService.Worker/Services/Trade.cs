using CryptoTraderService.Worker.Entities;
using CryptoTraderService.Worker.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoTraderService.Worker.Services
{
    public class Trade : ITrade
    {
        private readonly ILogger _logger;
        private readonly IRequest _request;
        private readonly TradeSettings _tradeSettings;

        public Trade
        (
            ILogger<Trade> logger,
            IRequest request,
            IConfiguration configuration
        )
        {
            _logger = logger;
            _request = request;
            _tradeSettings = configuration
                .GetSection(typeof(TradeSettings).Name).Get<TradeSettings>();
        }

        public Task Operation()
        {
            try
            {
                #region Orders

                var responseOrdersWaiting = _request
                    .SendRequest($"{_tradeSettings.Host}/v2/market/user_orders/list?status=waiting&start_date=&end_date=&pair=BRLETH&type=buy&page_size=1&current_page=1", _tradeSettings.Token, null, Method.GET, true);

                var ordersWating = JsonConvert.DeserializeObject<UserOrder>(responseOrdersWaiting);

                #endregion

                #region Balance

                var responseBalance = _request.SendRequest($"{_tradeSettings.Host}/v3/wallets/balance", _tradeSettings.Token, null, Method.GET);
                var balance = JsonConvert.DeserializeObject<Balance>(responseBalance);

                #endregion

                var brl = balance.Data.First(f => f.Currency_code == "BRL");
                var eth = balance.Data.First(f => f.Currency_code == "ETH");

                var amount = brl.Available_amount - _tradeSettings.LimitAmount;

                var responseEstimatedPrice = _request
                    .SendRequest($"{_tradeSettings.Host}/v2/market/estimated_price?amount=1&pair=BRLETH&type=buy", _tradeSettings.Token, null, Method.GET, true);

                var estimatedPrice = JsonConvert.DeserializeObject<EstimatedPrice>(responseEstimatedPrice).Data.Price;

                if (brl.Available_amount > _tradeSettings.LimitAmount && brl.Locked_amount == 0 && estimatedPrice < _tradeSettings.MinValue)
                {
                    var entity = new Order
                    {
                        Pair = "BRLETH",
                        Type = "buy",
                        Subtype = _tradeSettings.Subtype,
                        Amount = (float)(amount / estimatedPrice),
                        Unit_price = estimatedPrice + (float)0.0000001,
                        Request_price = amount
                    };

                    var json = JsonConvert.SerializeObject(entity);
                    var response = _request.SendRequest($"{_tradeSettings.Host}/v3/market/create_order", _tradeSettings.Token, json, Method.POST);

                    _logger.LogInformation(response);
                }

                else if (eth.Available_amount > 0 && eth.Locked_amount == 0 && estimatedPrice > _tradeSettings.MaxValue)
                {
                    var entity = new Order
                    {
                        Pair = "BRLETH",
                        Type = "sell",
                        Subtype = _tradeSettings.Subtype,
                        Amount = (float)(eth.Available_amount - 0.0000001),
                        Unit_price = estimatedPrice
                    };

                    var json = JsonConvert.SerializeObject(entity);
                    var response = _request.SendRequest($"{_tradeSettings.Host}/v3/market/create_order", _tradeSettings.Token, json, Method.POST);

                    _logger.LogInformation(response);
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return Task.FromException(e);
            }

            return Task.CompletedTask;
        }
    }
}
