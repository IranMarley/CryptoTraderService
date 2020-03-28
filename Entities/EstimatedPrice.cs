using System.Collections.Generic;

namespace CryptoTraderService.Entities
{
    public class EstimatedPriceDetail
    {
        public float price { get; set; }
    }

    public class EstimatedPrice
    {
        public string code { get; set; }
        public string message { get; set; }
        public EstimatedPriceDetail data { get; set; }

    }
}