using System;

namespace CryptoTraderService.Worker.Entities
{
    public class UserOrderDetail
    {
        public string Id { get; set; }
        public string Code { get; set; }
        public string Type { get; set; }
        public string Subtype { get; set; }
        public float Requested_amount { get; set; }
        public float Remaining_amount { get; set; }
        public float Unit_price { get; set; }
        public string Status { get; set; }
        public DateTime Create_date { get; set; }
        public DateTime Update_date { get; set; }
        public string Pair { get; set; }
        public float Total_price { get; set; }
        public float Executed_amount { get; set; }
        public float Remaining_price { get; set; }
    }
}