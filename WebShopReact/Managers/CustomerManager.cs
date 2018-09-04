using Microsoft.EntityFrameworkCore;
using System.Linq;
using WebShop.Models;

namespace WebShop.Managers
{
    public class CustomerManager
    {
        private WebShopContext _context;
        private readonly IContextHelper _contextHelper;

        public CustomerManager(IContextHelper contextHelper)
        {
            _contextHelper = contextHelper;
            _context = _contextHelper.SetContext();
        }

        public Customer GetCustomer(int id)
        {
            var customer = _context.Customer.FirstOrDefault(m => m.CustomerId == id);
            return customer;
        }

        public void AddCustomer(Customer customer)
        {
            try
            {
                _context.Add(customer);
                _context.SaveChanges();
            }
            catch
            {
                //TODO
            }
        }

        public void UpdateCustomer(Customer customer)
        {
            try
            {
                _context.Update(customer);
                _context.SaveChanges();
            }
            catch
            {
                //TODO
            }
        }

    }
}
