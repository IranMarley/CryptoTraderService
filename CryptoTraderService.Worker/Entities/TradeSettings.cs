namespace CryptoTraderService.Worker.Entities
{
    public class TradeSettings
    {
        public string Host { get; set; }
        public string Token { get; set; }
        public float AvailableAmount { get; set; }
        public float LimitAmount { get; set; }
        public float MinValue { get; set; }
        public float MaxValue { get; set; }
        public string Currency1 { get; set; }
        public string Currency2 { get; set; }
        public string Subtype { get; set; }
    }
}