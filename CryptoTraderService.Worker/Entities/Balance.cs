using System.Collections.Generic;

namespace CryptoTraderService.Worker.Entities
{
    public class BalanceDetail
    {
        public float available_amount { get; set; }
        public string currency_code { get; set; }
        public float locked_amount { get; set; }
    }

    public class Balance
    {
        public string code { get; set; }
        public string message { get; set; }
        public IEnumerable<BalanceDetail> data { get; set; }
    }
}