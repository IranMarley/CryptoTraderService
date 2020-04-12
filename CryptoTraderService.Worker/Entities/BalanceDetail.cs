using System.Collections.Generic;

namespace CryptoTraderService.Worker.Entities
{
    public class BalanceDetail
    {
        public float Available_amount { get; set; }
        public string Currency_code { get; set; }
        public float Locked_amount { get; set; }
    }
}