using Microsoft.AspNetCore.Mvc;
using WebShopReact.Models;
using WebShop.Managers;
using System.Collections.Generic;

namespace WebShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingCartController : Controller
    {
        private readonly ShoppingCartManager _shoppingCartManager;
        private readonly int _customer_id = 1;

        public ShoppingCartController(ShoppingCartManager shoppingCartManager)
        {
            _shoppingCartManager = shoppingCartManager;
        }

        // GET: ShoppingCart
        //[HttpGet]
        public IEnumerable<Product> Index()
        {
            var products = _shoppingCartManager.GetProductsFromCart(_customer_id);
            return products;
        }

        // DELETE: ShoppingCart/ProductId
        [HttpDelete("{ProductId}")]
        public IActionResult DeleteFromCart(int ProductId)
        {
            _shoppingCartManager.DeleteFromCart(_customer_id, ProductId);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult AddToCart([FromBody][Bind("ProductId,Name,Description,Price,Quantity")] Product product)
        {
            _shoppingCartManager.AddToCart(_customer_id, product);
            return CreatedAtAction("Details", "Products", new { id = product.ProductId });
        }
    }
}