using System.Collections.Generic;

namespace CryptoTraderService.Worker.Entities
{
    public class Summary
    {
        public string Message { get; set; }
        public IEnumerable<SummaryDetail> Data { get; set; }
    }
}