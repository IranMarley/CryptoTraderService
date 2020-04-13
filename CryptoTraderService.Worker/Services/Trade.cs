using CryptoTraderService.Worker.Entities;
using CryptoTraderService.Worker.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Linq;
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
                var pair = $"{_tradeSettings.Currency1}{_tradeSettings.Currency2}";

                #region Orders

                var responseOrdersWaiting = _request
                    .SendRequest($"{_tradeSettings.Host}/" +
                            $"{string.Format(_tradeSettings.GetUserOrdersEndpoint, pair, OrderType.Buy, 1, 1)}", 
                        _tradeSettings.Token, null, Method.GET, true);

                var ordersWating = JsonConvert.DeserializeObject<UserOrder>(responseOrdersWaiting);

                #endregion

                #region Balance

                var responseBalance = _request
                    .SendRequest($"{_tradeSettings.Host}/{_tradeSettings.GetBalanceEndpoint}", 
                        _tradeSettings.Token, null, Method.GET);

                var balance = JsonConvert.DeserializeObject<Balance>(responseBalance);

                #endregion

                var currence1 = balance.Data.First(f => f.Currency_code == _tradeSettings.Currency1);
                var currence2 = balance.Data.First(f => f.Currency_code == _tradeSettings.Currency2);

                var amount = currence1.Available_amount - _tradeSettings.LimitAmount;

                var responseEstimatedPrice = _request
                    .SendRequest($"{_tradeSettings.Host}/" +
                            $"{string.Format(_tradeSettings.GetPriceEndpoint, 1, pair, OrderType.Buy)}",
                        _tradeSettings.Token, null, Method.GET, true);

                var estimatedPrice = JsonConvert.DeserializeObject<EstimatedPrice>(responseEstimatedPrice).Data.Price;

                if (currence1.Available_amount > _tradeSettings.LimitAmount 
                    && currence1.Locked_amount == 0 
                    && estimatedPrice < _tradeSettings.MinValue)
                {
                    var entity = new Order
                    {
                        Pair = pair,
                        Type = OrderType.Buy,
                        Subtype = _tradeSettings.Subtype,
                        Amount = (float)(amount / estimatedPrice),
                        Unit_price = estimatedPrice + 0.0000001f,
                        Request_price = amount
                    };

                    var json = JsonConvert.SerializeObject(entity);

                    var response = _request
                        .SendRequest($"{_tradeSettings.Host}/{_tradeSettings.GetOrderEndpoint}", 
                            _tradeSettings.Token, json, Method.POST);

                    _logger.LogInformation(response);
                }

                else if (currence2.Available_amount > 0 
                    && currence2.Locked_amount == 0 
                    && estimatedPrice > _tradeSettings.MaxValue)
                {
                    var entity = new Order
                    {
                        Pair = pair,
                        Type = OrderType.Sell,
                        Subtype = _tradeSettings.Subtype,
                        Amount = (float)(currence2.Available_amount - 0.0000001),
                        Unit_price = estimatedPrice
                    };

                    var json = JsonConvert.SerializeObject(entity);

                    var response = _request
                        .SendRequest($"{_tradeSettings.Host}/{_tradeSettings.GetOrderEndpoint}", 
                            _tradeSettings.Token, json, Method.POST);

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
