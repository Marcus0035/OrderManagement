using Microsoft.EntityFrameworkCore;
using OrderManagement.Data.Models;

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

        public async Task<List<Customer>> GetCustomersAsync()
        {
            return await Customers.Where(x => !x.Deleted).ToListAsync();
        }

        public async Task RemoveCustomersAsync(IEnumerable<Customer> customers)
        {
            foreach (var customer in customers)
            {
                customer.Deleted = true;
            };

            await SaveChangesAsync();
        }
    }
}
