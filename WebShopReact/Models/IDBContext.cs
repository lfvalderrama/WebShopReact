using Microsoft.EntityFrameworkCore;

namespace WebShop.Models
{
    public interface IDBContext
    {
        DbSet<Customer> Customer { get; set; }
        DbSet<Order> Order { get; set; }
        DbSet<OrderDetail> OrderDetail { get; set; }
        DbSet<Product> Product { get; set; }
        DbSet<ProductCategory> ProductCategory { get; set; }
        DbSet<ProductProductCategory> ProductProductCategory { get; set; }

        int SaveChanges();
        //Task<int> SaveChangesAsync();
    }
}
