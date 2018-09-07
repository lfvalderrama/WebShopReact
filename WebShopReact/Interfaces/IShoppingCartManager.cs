using System.Collections.Generic;
using WebShopReact.Models;

namespace WebShopReact.Interfaces
{
    public interface IShoppingCartManager
    {
        List<Product> GetProductsFromCart(int customer_id);
        void DeleteFromCart(int customer_id, int ProductId);
        void AddToCart(int customer_id, Product product);
    }
}
