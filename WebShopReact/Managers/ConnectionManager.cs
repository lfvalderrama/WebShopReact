using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebShopReact.Helpers;
using WebShopReact.Models;

namespace WebShopReact.Managers
{
    public interface IConnectionManager
    {
        string SwitchConnection(string connection);
    }


    public class ConnectionManager : IConnectionManager
    {
        private readonly IHttpContextAccessor _accessor;
        private readonly ITokenHelper _tokenHelper;

        public ConnectionManager(ITokenHelper tokenHelper, IHttpContextAccessor accessor)
        {
            _tokenHelper = tokenHelper;
            _accessor = accessor;
        }

        public string SwitchConnection(string connection)
        {
            var customerId = Int32.Parse(_accessor.HttpContext.User.Identity.Name);
            var connectionType = (ConnectionTypes)System.Enum.Parse(typeof(ConnectionTypes), connection);
            var token = _tokenHelper.GetToken(customerId, connectionType);
            return token;
        }
    }
}
