using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace WebShop.Models
{
    public partial class WebShopContext : DbContext
    {
        public WebShopContext()
        {
        }

        public WebShopContext(DbContextOptions options)
            : base(options)
        {
        }

        public virtual DbSet<Customer> Customer { get; set; }
        public virtual DbSet<Order> Order { get; set; }
        public virtual DbSet<OrderProducts> OrderProducts { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<ProductCategory> ProductCategory { get; set; }
        public virtual DbSet<ProductProductCategory> ProductProductCategory { get; set; }
        public virtual DbSet<ShoppingCartProducts> ShoppingCartProducts { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("customer");

                entity.Property(e => e.CustomerId).HasColumnName("customer_Id");

                entity.Property(e => e.Age).HasColumnName("age");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("email")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasColumnName("first_Name")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasColumnName("last_Name")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("order");

                entity.Property(e => e.OrderId).HasColumnName("order_Id");

                entity.Property(e => e.CustomerId).HasColumnName("customer_Id");

                entity.Property(e => e.OrderDate)
                    .HasColumnName("order_date")
                    .HasColumnType("date");

                entity.Property(e => e.ShipAddress)
                    .IsRequired()
                    .HasColumnName("ship_address")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ShipDate)
                    .HasColumnName("ship_date")
                    .HasColumnType("date");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Order)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK_order_customer");
            });

            modelBuilder.Entity<OrderProducts>(entity =>
            {
                entity.HasKey(e => e.OrderProductId);

                entity.ToTable("order_products");

                entity.HasIndex(e => new { e.OrderId, e.ProductId })
                    .HasName("IX_order_detail");

                entity.Property(e => e.OrderProductId).HasColumnName("order_product_id");

                entity.Property(e => e.OrderId).HasColumnName("order_Id");

                entity.Property(e => e.ProductId).HasColumnName("product_Id");

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderProducts)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_order_detail_order");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.OrderProducts)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_order_detail_product");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("product");

                entity.Property(e => e.ProductId).HasColumnName("product_Id");

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasColumnType("text");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Price)
                    .HasColumnName("price")
                    .HasColumnType("money");

                entity.Property(e => e.Quantity).HasColumnName("quantity");
            });

            modelBuilder.Entity<ProductCategory>(entity =>
            {
                entity.ToTable("product_category");

                entity.Property(e => e.ProductCategoryId).HasColumnName("product_category_Id");

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ProductProductCategory>(entity =>
            {
                entity.ToTable("product_product_category");

                entity.HasIndex(e => new { e.ProductCategoryId, e.ProductId })
                    .HasName("IX_product_product_category");

                entity.Property(e => e.ProductProductCategoryId).HasColumnName("product_product_category_id");

                entity.Property(e => e.ProductCategoryId).HasColumnName("product_category_Id");

                entity.Property(e => e.ProductId).HasColumnName("product_Id");

                entity.HasOne(d => d.ProductCategory)
                    .WithMany(p => p.ProductProductCategory)
                    .HasForeignKey(d => d.ProductCategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_product_product_category_product_category");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ProductProductCategory)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_product_product_category_product");
            });

            modelBuilder.Entity<ShoppingCartProducts>(entity =>
            {
                entity.ToTable("shopping_cart_products");

                entity.HasIndex(e => new { e.ProductId, e.CustomerId })
                    .HasName("IX_shopping_cart_products");

                entity.Property(e => e.ShoppingCartProductsId).HasColumnName("Shopping_cart_products_Id");

                entity.Property(e => e.CustomerId).HasColumnName("customer_Id");

                entity.Property(e => e.ProductId).HasColumnName("product_Id");

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.ShoppingCartProducts)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_shopping_cart_products_customer");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ShoppingCartProducts)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_shopping_cart_products_product1");
            });
        }
    }
}
