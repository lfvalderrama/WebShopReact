using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Security.Claims;
using WebShopReact.Helpers;
using WebShopReact.Interfaces;
using WebShopReact.Models;

namespace WebShopReact.Managers
{
    public class ConnectionManager : IConnectionManager
    {
        private readonly IHttpContextAccessor _accessor;
        private readonly ITokenHelper _tokenHelper;
        private readonly ICustomerManager _customerManager;
        private readonly IContextHelper _contextHelper;
        private WebShopContext _context;

        public ConnectionManager(ITokenHelper tokenHelper, IHttpContextAccessor accessor, ICustomerManager customerManager, IContextHelper contextHelper)
        {
            _tokenHelper = tokenHelper;
            _accessor = accessor;
            _customerManager = customerManager;
            _contextHelper = contextHelper;
            _context = _contextHelper.SetContext();
        }

        public string SwitchConnection(string connection)
        {
            var customerId = Int32.Parse(_accessor.HttpContext.User.Identity.Name);
            var current_customer = _context.Customer.Where(c => c.CustomerId == customerId).FirstOrDefault();
            var identity = _accessor.HttpContext.User.Identity as ClaimsIdentity; ;
            identity.RemoveClaim(identity.FindFirst("Database"));
            identity.AddClaim(new Claim("Database", connection));
            _context = _contextHelper.SetContext();
            if(current_customer != null && !_context.Customer.Where(c=>c.CustomerId == current_customer.CustomerId).Any() )
            {
                _context.Add(current_customer);
                _context.SaveChanges();
            }           
            var connectionType = (ConnectionTypes)System.Enum.Parse(typeof(ConnectionTypes), connection);
            var token = _tokenHelper.GetToken(customerId, connectionType);
            return token;
        }
    }
}
