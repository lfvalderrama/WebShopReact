using System;
using System.Collections.Generic;

namespace WebShop.Models
{
    public partial class ProductProductCategory
    {
        public int ProductCategoryId { get; set; }
        public int ProductId { get; set; }
        public int ProductProductCategoryId { get; set; }

        public Product Product { get; set; }
        public ProductCategory ProductCategory { get; set; }
    }
}
