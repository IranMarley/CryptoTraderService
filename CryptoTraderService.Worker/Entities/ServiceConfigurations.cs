namespace CryptoTraderService.Worker.Entities
{
    public class ServiceConfigurations
    {
        public string Host { get; set; }
        public string Token { get; set; }
        public int Interval { get; set; }
        public float AvailableAmount { get; set; }
        public float LimitAmount { get; set; }
        public float MinValue { get; set; }
        public float MaxValue { get; set; }
    }
}