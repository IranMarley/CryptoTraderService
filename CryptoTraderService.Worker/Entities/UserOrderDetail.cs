using Newtonsoft.Json;
using System;

namespace CryptoTraderService.Worker.Entities
{
    public class UserOrderDetail
    {
        public string Id { get; set; }
        public string Code { get; set; }
        public string Type { get; set; }
        public string Subtype { get; set; }

        [JsonProperty(PropertyName = "Requested_amount")]
        public float RequestedAmount { get; set; }

        [JsonProperty(PropertyName = "Remaining_amount")]
        public float RemainingAmount { get; set; }

        [JsonProperty(PropertyName = "Unit_price")]
        public float UnitPrice { get; set; }

        [JsonProperty(PropertyName = "Status")]
        public string Status { get; set; }

        [JsonProperty(PropertyName = "Create_date")]
        public DateTime CreateDate { get; set; }

        [JsonProperty(PropertyName = "Update_date")]
        public DateTime UpdateDate { get; set; }

        [JsonProperty(PropertyName = "Pair")]
        public string Pair { get; set; }

        [JsonProperty(PropertyName = "Total_price")]
        public float TotalPrice { get; set; }

        [JsonProperty(PropertyName = "Executed_amount")]
        public float ExecutedAmount { get; set; }

        [JsonProperty(PropertyName = "Remaining_price")]
        public float RemainingPrice { get; set; }
    }
}