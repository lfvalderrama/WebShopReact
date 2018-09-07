using System.Collections.Generic;
using System.Linq;
using WebShopReact.Helpers;
using WebShopReact.Interfaces;
using WebShopReact.Models;

namespace WebShopReact.Managers
{
    public class ProductManager : IProductManager
    {
        private readonly IContextHelper _contextHelper;
        private WebShopContext _context;

        public ProductManager(IContextHelper contextHelper)
        {
            _contextHelper = contextHelper;
            _context = _contextHelper.SetContext();
        }

        public List<Product> GetAllProducts()
        {
            var products = _context.Product.ToList();
            return products;
        }

        public Product GetProduct(int id)
        {
            var product = _context.Product.FirstOrDefault(m => m.ProductId == id);
            return product;
        }

        public void AddProduct(Product product)
        {
            try
            {
                _context.Add(product);
                _context.SaveChanges();
            }
            catch
            {
                //TODO
            }
        }

        public void UpdateProduct(Product product)
        {
            try
            {
                _context.Update(product);
                _context.SaveChanges();
            }
            catch
            {
                //TODO
            }
        }

        public void DeleteProduct(int id)
        {
            try
            {
                var product = GetProduct(id);
                var productOnCart = _context.ShoppingCartProducts.Where(scp => scp.Product.ProductId == id);
                foreach (var row in productOnCart)
                {
                    _context.ShoppingCartProducts.Remove(row);
                }
                _context.Product.Remove(product);
                _context.SaveChanges();
            }
            catch
            {
                //TODO
            }
        }
    }
}
