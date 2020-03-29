namespace CryptoTraderService.Worker.Entities
{
    public class Order
    {
        public string pair { get; set; }
        public float amount { get; set; }
        public string type { get; set; }
        public string subtype { get; set; }
        public float unit_price { get; set; }
        public float request_price { get; set; }
    }
}