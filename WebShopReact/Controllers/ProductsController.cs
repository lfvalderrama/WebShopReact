using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebShopReact.Interfaces;
using WebShopReact.Models;

namespace WebShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : Controller
    {
        private readonly IProductManager _productManager;

        public ProductsController(IProductManager productManager)
        {
            _productManager = productManager;
        }

        // GET: api/Products
        public IEnumerable<Product> Index()
        {
            var products = _productManager.GetAllProducts();
            return products;
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public IActionResult Details(int? id)
        {
            var product = _productManager.GetProduct((int)id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        // POST: api/Products/
        [HttpPost]
       // [ValidateAntiForgeryToken]
        public IActionResult Create([FromBody] Product product)
        {
            if (ModelState.IsValid)
            {
                _productManager.AddProduct(product);
                return CreatedAtAction("Details", new { id = product.ProductId }, product);
            }
            return BadRequest(ModelState); 
        }

        // PUT: api/Products/5
        [HttpPut("{id}")]
        public IActionResult Edit(int id, [FromBody][Bind("ProductId,Name,Description,Price,Quantity")] Product product)
        {
            if (id != product.ProductId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _productManager.UpdateProduct(product);
                return RedirectToAction(nameof(Index));
            }
            return BadRequest(ModelState);
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public IActionResult DeleteConfirmed(int id)
        {
            var product = _productManager.GetProduct(id);
            if (product == null)
            {
                return NotFound();
            }
            _productManager.DeleteProduct(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
