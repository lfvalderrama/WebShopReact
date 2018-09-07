using System;
using System.Collections.Generic;

namespace WebShopReact.Models
{
    public partial class Order
    {
        public Order()
        {
            OrderProducts = new HashSet<OrderProducts>();
        }

        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime? ShipDate { get; set; }
        public string ShipAddress { get; set; }
        public int? CustomerId { get; set; }

        public Customer Customer { get; set; }
        public ICollection<OrderProducts> OrderProducts { get; set; }
    }
}
