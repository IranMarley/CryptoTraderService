using Newtonsoft.Json;

namespace CryptoTraderService.Worker.Entities
{
    public class BalanceDetail
    {
        [JsonProperty(PropertyName = "available_amount")]
        public float AvailableAmount { get; set; }

        [JsonProperty(PropertyName = "currency_code")]
        public string CurrencyCode { get; set; }

        [JsonProperty(PropertyName = "locked_amount")]
        public float LockedAmount { get; set; }
    }
}