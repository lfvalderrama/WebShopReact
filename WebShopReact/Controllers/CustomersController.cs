using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebShop.Managers;
using WebShop.Models;

namespace WebShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : Controller
    {
        private readonly CustomerManager _customerManager;

        public CustomersController(CustomerManager customerManager)
        {
            _customerManager = customerManager;
        }

        // GET: Customers/Details/5
        [HttpGet("{id}")]
        public IActionResult Details(int? id)
        {
            var customer = _customerManager.GetCustomer((int)id);
            if (customer == null)
            {
                return NotFound();
            }
            return Ok(customer);
        }

        // POST: Customers/Create
        [HttpPost]
        public IActionResult Create([FromBody][Bind("CustomerId,FirstName,LastName,Email,Age,ShoppingCartId")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                _customerManager.AddCustomer(customer);
                return CreatedAtAction("Details", new { id = customer.CustomerId });
            }
            return BadRequest(ModelState);
        }
        // POST: Customers/Edit/5
        [HttpPut("{id}")]
        public IActionResult Edit(int id, [Bind("CustomerId,FirstName,LastName,Email,Age,ShoppingCartId")] Customer customer)
        {
            if (id != customer.CustomerId)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                _customerManager.UpdateCustomer(customer);
                return Ok();
            }
            return BadRequest(ModelState);
        }     
    }
}
