namespace CryptoTraderService.Worker.Entities
{
    public class EstimatedPrice
    {
        public string Code { get; set; }
        public string Message { get; set; }
        public EstimatedPriceDetail Data { get; set; }
    }
}