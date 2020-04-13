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

        public Order Create
        (
            string pair, 
            float amount, 
            string type, 
            string subType, 
            float unitPrice, 
            float requestPrice
        ) => 
            new Order
            {
                Pair = pair,
                Type = type,
                Subtype = subType,
                Amount = amount,
                Unit_price = unitPrice,
                Request_price = requestPrice
            };
    }
}