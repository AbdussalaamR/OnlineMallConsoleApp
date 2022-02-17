using Microsoft.EntityFrameworkCore;

namespace ECommerce
{
    public class ECommerceContext:DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL("server=localhost;database=ECommerce;user=root;password=Ar1coaster*");
        }
        
        public  DbSet<Customer> Customers { get; set; }
        public  DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Admin> Admin { get; set; }
        public DbSet<OrderProduct> OrderProducts { get; set; }
    }
}