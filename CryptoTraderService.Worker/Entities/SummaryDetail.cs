using System.Collections.Generic;

namespace CryptoTraderService.Worker.Entities
{
    public class SummaryDetail
    {
        public float Unit_price_24h { get; set; }
        public float Volume_24h { get; set; }
        public float Last_transaction_unit_price { get; set; }
        public string Pair { get; set; }
        public float Max_value { get; set; }
        public float Min_value { get; set; }
    }
}