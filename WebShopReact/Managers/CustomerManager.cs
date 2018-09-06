using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using WebShopReact.Helpers;
using WebShopReact.Models;

namespace WebShop.Managers
{
    public class CustomerManager
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

        public object Authenticate(CustomerDTO customerIn)
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

            var token = _tokenHelper.GetToken(customer);
            
            return new { token = token};
        }        

        public Customer GetCustomer(int id)
        {
            var customer = _context.Customer.FirstOrDefault(m => m.CustomerId == id);
            return customer;
        }

        public void AddCustomer(CustomerDTO customerIn)
        {
            var customer = _mapper.Map<Customer>(customerIn);
            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(customerIn.Password, out passwordHash, out passwordSalt);

            customer.PasswordHash = passwordHash;
            customer.PasswordSalt = passwordSalt;
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
