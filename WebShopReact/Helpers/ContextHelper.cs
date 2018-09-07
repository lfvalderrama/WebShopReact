using Autofac.Features.Indexed;
using Microsoft.AspNetCore.Http;
using System.Linq;
using WebShopReact.Models;

namespace WebShopReact.Helpers
{
    public interface IContextHelper
    {
        WebShopContext SetContext();
    }

    public class ContextHelper : IContextHelper
    {
        private readonly IHttpContextAccessor _accessor;
        private WebShopContext _context;
        private readonly IIndex<ConnectionTypes, WebShopContext> _contexts;
        private readonly ConnectionTypes defaultConnection = ConnectionTypes.SqlServer;

        public ContextHelper(IHttpContextAccessor accessor, IIndex<ConnectionTypes, WebShopContext> contexts)
        {
            _accessor = accessor;
            _contexts = contexts;
            _context = _contexts[defaultConnection];
        }

        public WebShopContext SetContext()
        {
            var userClaim = _accessor.HttpContext.User.Claims.Where(c => c.Type == "Database").FirstOrDefault();
            var type = ConnectionTypes.SqlServer.ToString();
            if (userClaim != null) type = userClaim.Value;
            var connectionType = (ConnectionTypes)System.Enum.Parse(typeof(ConnectionTypes), type);
            _context = _contexts[connectionType];
            return _context;
        }
    }
}
