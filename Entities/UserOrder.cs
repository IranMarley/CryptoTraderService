using System;
using System.Collections.Generic;

namespace CryptoTraderService.Entities
{
    public class UserOrderDetail
    {
        public string id { get; set; }
        public string code { get; set; }
        public string type { get; set; }
        public string subtype { get; set; }
        public float requested_amount { get; set; }
        public float remaining_amount { get; set; }
        public float unit_price { get; set; }
        public string status { get; set; }
        public DateTime create_date { get; set; }
        public DateTime update_date { get; set; }
        public string pair { get; set; }
        public float total_price { get; set; }
        public float executed_amount { get; set; }
        public float remaining_price { get; set; }


    }

    public class UserOrder
    {
        public string message { get; set; }
        public IEnumerable<UserOrderDetail> orders { get; set; } = new List<UserOrderDetail>();

    }


}