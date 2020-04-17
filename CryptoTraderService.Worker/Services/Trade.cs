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

        public T CallEndpoint<T>(string endpoint, string pair) =>
            _request.SendRequest<T>($"{_tradeSettings.Host}/" +
                    $"{string.Format(endpoint, pair, OrderType.Buy, 1, 1)}",
                _tradeSettings.Token, null, Method.GET, true);

        public T CallEndpoint<T>(string endpoint, string pair, int amount) =>
            _request.SendRequest<T>($"{_tradeSettings.Host}/" +
                    $"{string.Format(endpoint, amount, pair, OrderType.Buy)}",
                _tradeSettings.Token, null, Method.GET, true);

        public T CallEndpoint<T>(string endpoint, Method method, string json) =>
            _request.SendRequest<T>($"{_tradeSettings.Host}/{endpoint}",
                _tradeSettings.Token, json, method);

        public Task Operation()
        {
            try
            {
                var pair = $"{_tradeSettings.Currency1}{_tradeSettings.Currency2}";

                var ordersWating = CallEndpoint<UserOrder>(_tradeSettings.GetUserOrdersEndpoint, pair);

                var balance = CallEndpoint<Balance>(_tradeSettings.GetBalanceEndpoint, Method.GET, null);

                var estimatedPrice = CallEndpoint<EstimatedPrice>(_tradeSettings.GetPriceEndpoint, pair, 1);

                var currence1 = GetCurrenceData(balance, _tradeSettings.Currency1);
                var currence2 = GetCurrenceData(balance, _tradeSettings.Currency2);
                var amount = currence1.AvailableAmount - _tradeSettings.LimitAmount;
                var price = estimatedPrice.Data.Price;

                if (currence1.AvailableAmount > _tradeSettings.LimitAmount
                    && currence1.LockedAmount == 0
                    && price < _tradeSettings.MinValue)
                {
                    var newAmount = amount / price;
                    var newUnitPrice = price + 0.0000001f;

                    var entity = CreateOrder(pair, OrderType.Buy, newAmount,
                        _tradeSettings.Subtype, newUnitPrice, amount);

                    var response = CallEndpoint<object>(_tradeSettings.GetOrderEndpoint,
                        Method.POST, JsonConvert.SerializeObject(entity));

                    _logger.LogInformation(response.ToString());
                }
                else if (currence2.AvailableAmount > 0
                    && currence2.LockedAmount == 0
                    && price > _tradeSettings.MaxValue)
                {
                    var newAmount = currence2.AvailableAmount - 0.0000001f;

                    var entity = CreateOrder(pair, OrderType.Sell,
                        newAmount, _tradeSettings.Subtype, price, 0);

                    var response = CallEndpoint<object>(_tradeSettings.GetOrderEndpoint,
                        Method.POST, JsonConvert.SerializeObject(entity));

                    _logger.LogInformation(response.ToString());
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return Task.FromException(e);
            }

            return Task.CompletedTask;
        }

        private BalanceDetail GetCurrenceData(Balance balance, string currency) =>
            balance.Data.First(f => f.CurrencyCode == currency);

        private Order CreateOrder
        (
            string pair,
            string type,
            float amount,
            string subType,
            float unitPrice,
            float requestPrice
        ) => new Order().Create(pair, amount, type, subType, unitPrice, requestPrice);
    }
}
