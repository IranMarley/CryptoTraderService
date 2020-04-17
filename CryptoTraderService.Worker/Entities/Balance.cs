using System.Collections.Generic;

namespace CryptoTraderService.Worker.Entities
{
    public class Balance
    {
        public string Code { get; set; }
        public string Message { get; set; }
        public IEnumerable<BalanceDetail> Data { get; set; }
    }
}