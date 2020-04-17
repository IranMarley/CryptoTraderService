using Newtonsoft.Json;

namespace CryptoTraderService.Worker.Entities
{
    public class SummaryDetail
    {
        [JsonProperty(PropertyName = "unit_price_24h")]
        public float UnitPrice24h { get; set; }

        [JsonProperty(PropertyName = "volume_24h")]
        public float Volume24h { get; set; }

        [JsonProperty(PropertyName = "last_transaction_unit_price")]
        public float LastTransactionUnitPrice { get; set; }

        [JsonProperty(PropertyName = "pair")]
        public string Pair { get; set; }

        [JsonProperty(PropertyName = "max_value")]
        public float MaxValue { get; set; }

        [JsonProperty(PropertyName = "min_value")]
        public float MinValue { get; set; }
    }
}