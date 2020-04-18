using Newtonsoft.Json;
using System;

namespace CryptoTraderService.Worker.Entities
{
    public class UserOrderDetail
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "code")]
        public string Code { get; set; }

        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }

        [JsonProperty(PropertyName = "subtype")]
        public string Subtype { get; set; }

        [JsonProperty(PropertyName = "requested_amount")]
        public float RequestedAmount { get; set; }

        [JsonProperty(PropertyName = "remaining_amount")]
        public float RemainingAmount { get; set; }

        [JsonProperty(PropertyName = "unit_price")]
        public float UnitPrice { get; set; }

        [JsonProperty(PropertyName = "status")]
        public string Status { get; set; }

        [JsonProperty(PropertyName = "create_date")]
        public DateTime CreateDate { get; set; }

        [JsonProperty(PropertyName = "update_date")]
        public DateTime UpdateDate { get; set; }

        [JsonProperty(PropertyName = "pair")]
        public string Pair { get; set; }

        [JsonProperty(PropertyName = "total_price")]
        public float TotalPrice { get; set; }

        [JsonProperty(PropertyName = "executed_amount")]
        public float ExecutedAmount { get; set; }

        [JsonProperty(PropertyName = "remaining_price")]
        public float RemainingPrice { get; set; }
    }
}