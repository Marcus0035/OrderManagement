using Microsoft.EntityFrameworkCore;

namespace OrderManagement.Data
{
    public class OrderManagementContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=localhost;Database=OrderManagementDb;Trusted_Connection=True;TrustServerCertificate=True");
        }
    }
}
