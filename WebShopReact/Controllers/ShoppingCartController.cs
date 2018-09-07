using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using WebShopReact.Interfaces;
using WebShopReact.Models;

namespace WebShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ShoppingCartController : Controller
    {
        private readonly IShoppingCartManager _shoppingCartManager;
        private int _customer_id;

        public ShoppingCartController(IShoppingCartManager shoppingCartManager)
        {
            _shoppingCartManager = shoppingCartManager;
        }

        // GET: ShoppingCart
        //[HttpGet]
        public IEnumerable<Product> Index()
        {
            _customer_id = Int32.Parse(User.Identity.Name);
            var products = _shoppingCartManager.GetProductsFromCart(_customer_id);
            return products;
        }

        // DELETE: ShoppingCart/ProductId
        [HttpDelete("{ProductId}")]
        public IActionResult DeleteFromCart(int ProductId)
        {
            _customer_id = Int32.Parse(User.Identity.Name);
            _shoppingCartManager.DeleteFromCart(_customer_id, ProductId);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult AddToCart([FromBody][Bind("ProductId,Name,Description,Price,Quantity")] Product product)
        {
            _customer_id = Int32.Parse(User.Identity.Name);
            var test = User.Claims.Where(c => c.Type == "Database").FirstOrDefault();
            _shoppingCartManager.AddToCart(_customer_id, product);
            return CreatedAtAction("Details", "Products", new { id = product.ProductId });
        }
    }
}