using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebShop.Managers;
using WebShop.Models;

namespace WebShop.Controllers
{
    public class CustomersController : Controller
    {
        private readonly CustomerManager _customerManager;

        public CustomersController(CustomerManager customerManager)
        {
            _customerManager = customerManager;
        }

        // GET: Customers/Details/5
        public IActionResult Details(int? id)
        {
            var customer = _customerManager.GetCustomer((int)id);
            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }

        // GET: Customers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("CustomerId,FirstName,LastName,Email,Age,ShoppingCartId")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                _customerManager.AddCustomer(customer);
                return RedirectToAction("Details", new { id = customer.CustomerId });
            }
            return View(customer);
        }

        // GET: Customers/Edit/5
        public IActionResult Edit(int? id)
        {
            var customer = _customerManager.GetCustomer((int)id);
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("CustomerId,FirstName,LastName,Email,Age,ShoppingCartId")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                _customerManager.UpdateCustomer(customer);
                return RedirectToAction("Details", new { id = customer.CustomerId});
            }
            return View(customer);
        }     
    }
}
