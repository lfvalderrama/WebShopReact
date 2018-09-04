using System;
using System.Collections.Generic;

namespace WebShop.Models
{
    public partial class ProductCategory
    {
        public ProductCategory()
        {
            ProductProductCategory = new HashSet<ProductProductCategory>();
        }

        public int ProductCategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<ProductProductCategory> ProductProductCategory { get; set; }
    }
}
