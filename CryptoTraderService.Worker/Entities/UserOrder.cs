using System;
using System.Collections.Generic;

namespace CryptoTraderService.Worker.Entities
{
    public class UserOrder
    {
        public string Message { get; set; }
        public IEnumerable<UserOrderDetail> Orders { get; set; } = new List<UserOrderDetail>();
    }
}