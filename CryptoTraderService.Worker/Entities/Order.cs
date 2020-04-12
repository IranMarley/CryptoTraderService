namespace CryptoTraderService.Worker.Entities
{
    public class Order
    {
        public string Pair { get; set; }
        public float Amount { get; set; }
        public string Type { get; set; }
        public string Subtype { get; set; }
        public float Unit_price { get; set; }
        public float Request_price { get; set; }
    }
}