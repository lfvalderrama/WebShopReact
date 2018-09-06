using System;
using System.Collections.Generic;

namespace WebShopReact.Models
{
    public partial class ShoppingCartProducts
    {
        public int ProductId { get; set; }
        public int? Quantity { get; set; }
        public int CustomerId { get; set; }
        public int ShoppingCartProductsId { get; set; }

        public Customer Customer { get; set; }
        public Product Product { get; set; }
    }
}
