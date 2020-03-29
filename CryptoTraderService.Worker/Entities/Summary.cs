using System.Collections.Generic;

namespace CryptoTraderService.Worker.Entities
{
    public class SummaryDetail
    {
        public float unit_price_24h { get; set; }
        public float volume_24h { get; set; }
        public float last_transaction_unit_price { get; set; }
        public string pair { get; set; }
        public float max_value { get; set; }
        public float min_value { get; set; }
    }

    public class Summary
    {
        public string message { get; set; }
        public IEnumerable<SummaryDetail> data { get; set; }
    }
}