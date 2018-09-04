using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace WebShop.Models
{
    public class DBContextFactory
    {
        private IConfiguration _configuration;

        public DBContextFactory(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public WebShopContext CreateContext(ConnectionTypes type)
        {
            var options = new DbContextOptionsBuilder();
            if (type == ConnectionTypes.InMemory)
                options.UseInMemoryDatabase("inMemory");
            else
                options.UseSqlServer(_configuration.GetConnectionString("SQLServer"));
            return new WebShopContext(options.Options);
        }
    }
}
