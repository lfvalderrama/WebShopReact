using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebShopReact.Managers;
using WebShopReact.Models;

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

        // POST: Customers/authenticate
        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody]LoginInput customer)
        {
            var response = _customerManager.Authenticate(customer);
            if (response == null) return BadRequest(new { error = "Email or password is incorrect" });
            return Ok(response);
        }

        // GET: Customers/Details/5
        [HttpGet("{id}")]
        [Authorize]
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
        [AllowAnonymous]
        public IActionResult Create([FromBody][Bind("CustomerId,FirstName,LastName,Email,Age,ShoppingCartId")] CustomerDTO customer)
        {
            if (ModelState.IsValid)
            {
                var created = _customerManager.AddCustomer(customer);
                if (created)
                    return CreatedAtAction("Details", new { id = customer.CustomerId });
                else
                    return BadRequest(new { Error = "Email Already Taken" });
            }
            return BadRequest(ModelState);
        }
        // POST: Customers/Edit/5
        [HttpPut("{id}")]
        [Authorize]
        public IActionResult Edit(int id, [Bind("CustomerId,FirstName,LastName,Email,Age,ShoppingCartId")] Customer customer)
        {
            if (id.ToString() != User.Identity.Name)
            {
                return Unauthorized();
            }
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
