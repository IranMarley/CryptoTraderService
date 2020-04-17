using Newtonsoft.Json;

namespace CryptoTraderService.Worker.Entities
{
    public class Order
    {
        [JsonProperty(PropertyName = "pair")]
        public string Pair { get; set; }

        [JsonProperty(PropertyName = "amount")]
        public float Amount { get; set; }

        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }

        [JsonProperty(PropertyName = "subtype")]
        public string Subtype { get; set; }

        [JsonProperty(PropertyName = "unit_price")]
        public float UnitPrice { get; set; }

        [JsonProperty(PropertyName = "request_price")]
        public float RequestPrice { get; set; }

        public Order Create
        (
            string pair,
            float amount,
            string type,
            string subType,
            float unitPrice,
            float requestPrice
        ) =>
            new Order
            {
                Pair = pair,
                Type = type,
                Subtype = subType,
                Amount = amount,
                UnitPrice = unitPrice,
                RequestPrice = requestPrice
            };
    }
}