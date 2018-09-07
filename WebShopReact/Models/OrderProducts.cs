using System;
using System.Collections.Generic;

namespace WebShopReact.Models
{
    public partial class OrderProducts
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int? Quantity { get; set; }
        public int OrderProductId { get; set; }

        public Order Order { get; set; }
        public Product Product { get; set; }
    }
}
