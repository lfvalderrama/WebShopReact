using Autofac.Features.Indexed;
using Microsoft.AspNetCore.Http;
using WebShop.Models;

namespace WebShop.Managers
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
            var type = _accessor.HttpContext.Session.GetString("connection");
            var connectionType = ConnectionTypes.SqlServer;
            if (type != null) connectionType = (ConnectionTypes)System.Enum.Parse(typeof(ConnectionTypes), type);
            else _accessor.HttpContext.Session.SetString("connection", ConnectionTypes.SqlServer.ToString());
            _context = _contexts[connectionType];
            return _context;
        }
    }
}
