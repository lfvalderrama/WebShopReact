using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace WebShopReact.Models
{
    public partial class Product
    {
        public Product()
        {
            OrderProducts = new HashSet<OrderProducts>();
            ProductProductCategory = new HashSet<ProductProductCategory>();
            ShoppingCartProducts = new HashSet<ShoppingCartProducts>();
        }

        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal? Price { get; set; }
        public int? Quantity { get; set; }

        [JsonIgnore]
        public ICollection<OrderProducts> OrderProducts { get; set; }
        [JsonIgnore]
        public ICollection<ProductProductCategory> ProductProductCategory { get; set; }
        [JsonIgnore]
        public ICollection<ShoppingCartProducts> ShoppingCartProducts { get; set; }
    }
}
