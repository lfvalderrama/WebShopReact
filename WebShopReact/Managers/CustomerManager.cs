using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using WebShopReact.Helpers;
using WebShopReact.Interfaces;
using WebShopReact.Models;

namespace WebShopReact.Managers
{
    public class CustomerManager : ICustomerManager
    {
        private WebShopContext _context;
        private readonly IContextHelper _contextHelper;
        private readonly IMapper _mapper;
        private readonly ITokenHelper _tokenHelper;

        public CustomerManager(IContextHelper contextHelper, IMapper mapper, ITokenHelper tokenHelepr)
        {
            _contextHelper = contextHelper;
            _context = _contextHelper.SetContext();
            _mapper = mapper;
            _tokenHelper = tokenHelepr;
        }

        public object Authenticate(LoginInput customerIn)
        {
            var email = customerIn.Email;
            var password = customerIn.Password;
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
                return null;

            var customer = _context.Customer.FirstOrDefault(c => c.Email == email);

            if (customer == null)
                return null;

            if (!VerifyPasswordHash(password, customer.PasswordHash, customer.PasswordSalt))
                return null;

            var token = _tokenHelper.GetToken(customer.CustomerId, ConnectionTypes.SqlServer);
            
            return new { token = token, customerId = customer.CustomerId};
        }        

        public Customer GetCustomer(int id)
        {
            var customer = _context.Customer.FirstOrDefault(m => m.CustomerId == id);
            return customer;
        }

        public bool AddCustomer(CustomerDTO customerIn)
        {
            var customer = _mapper.Map<Customer>(customerIn);
            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(customerIn.Password, out passwordHash, out passwordSalt);

            customer.PasswordHash = passwordHash;
            customer.PasswordSalt = passwordSalt;

            if (_context.Customer.Where(c=>c.Email == customerIn.Email).Any())
            {
                return false;
            }

            try
            {
                _context.Add(customer);
                _context.SaveChanges();
                return true;
            }
            catch 
            {
                return false;
                //TODO
            }
        }

        public void UpdateCustomer(Customer customer)
        {
            var customerExistent = _context.Customer.AsNoTracking().Where(c => c.CustomerId == customer.CustomerId).FirstOrDefault();
            customer.PasswordHash = customerExistent.PasswordHash;
            customer.PasswordSalt = customerExistent.PasswordSalt;
            try
            {
                _context.Attach(customer);
                _context.Customer.Update(customer);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                //TODO
            }
        }

        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");

            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");
            if (storedHash.Length != 64) throw new ArgumentException("Invalid length of password hash (64 bytes expected).", "passwordHash");
            if (storedSalt.Length != 128) throw new ArgumentException("Invalid length of password salt (128 bytes expected).", "passwordHash");

            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i]) return false;
                }
            }

            return true;
        }

    }
}
