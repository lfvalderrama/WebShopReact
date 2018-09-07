using System.Collections.Generic;
using WebShopReact.Models;

namespace WebShopReact.Interfaces
{
    public interface IProductManager
    {
        List<Product> GetAllProducts();
        Product GetProduct(int id);
        void AddProduct(Product product);
        void UpdateProduct(Product product);
        void DeleteProduct(int id);
    }
}
