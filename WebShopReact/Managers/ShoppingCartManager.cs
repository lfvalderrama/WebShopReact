using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using WebShop.Models;

namespace WebShop.Managers
{
    public class ShoppingCartManager
    {
        private readonly IContextHelper _contextHelper;
        private WebShopContext _context;

        public ShoppingCartManager(IContextHelper contextHelper)
        {
            _contextHelper = contextHelper;
            _context = _contextHelper.SetContext();

        }

        public List<Product> GetProductsFromCart(int customer_id)
        {
            try
            {
                var shoppingCartProducts = _context.ShoppingCartProducts.Include(scp => scp.Product).Where(scp => scp.CustomerId == customer_id);
                var products = new List<Product>();
                foreach (var scp in shoppingCartProducts)
                {
                    var product = scp.Product;
                    product.Quantity = scp.Quantity;
                    products.Add(scp.Product);
                }
                return products;
            }
            catch 
            {
                return new List<Product>();
            }
        }

        public void DeleteFromCart(int customer_id, int ProductId)
        {
            try
            {
                var shoppingCartProduct = _context.ShoppingCartProducts.Where(scp => scp.CustomerId == customer_id && scp.ProductId == ProductId).FirstOrDefault();
                _context.ShoppingCartProducts.Remove(shoppingCartProduct);
                _context.SaveChanges();
            }
            catch
            {
                //TODO
            }
        }

        public void AddToCart(int customer_id, Product product)
        {
            try
            {
                var shoppingCartProducts = _context.ShoppingCartProducts.Where(sc => sc.CustomerId == customer_id && sc.ProductId == product.ProductId).FirstOrDefault();

                if (shoppingCartProducts != null)
                {
                    shoppingCartProducts.Quantity = product.Quantity;
                }
                else
                {
                    shoppingCartProducts = new ShoppingCartProducts
                    {
                        ProductId = product.ProductId,
                        Quantity = product.Quantity,
                        CustomerId = customer_id
                    };
                    _context.ShoppingCartProducts.Add(shoppingCartProducts);
                }
                _context.SaveChanges();
            }
            catch
            {
                //Todo
            }
        }
    }
}
