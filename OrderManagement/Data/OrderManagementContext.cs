using Microsoft.EntityFrameworkCore;
using OrderManagement.Data.Models;

namespace OrderManagement.Data
{
    public class OrderManagementContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Item> Items { get; set; }
        DbSet<ItemOrder> ItemOrders { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=localhost;Database=OrderManagementDb;Trusted_Connection=True;TrustServerCertificate=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ItemOrder>()
                .HasKey(io => new { io.OrderId, io.ItemId });

            modelBuilder.Entity<ItemOrder>()
                .HasOne(io => io.Order)
                .WithMany(o => o.ItemOrders)
                .HasForeignKey(io => io.OrderId);

            modelBuilder.Entity<ItemOrder>()
                .HasOne(io => io.Item)
                .WithMany(i => i.ItemOrders)
                .HasForeignKey(io => io.ItemId);
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

        public async Task AddCustomerAsync(Customer customer)
        {
            await Customers.AddAsync(customer);
            await SaveChangesAsync();
        }

        public async Task PostCustomerAsync(Customer customer)
        {
            Customers.Update(customer);
            await SaveChangesAsync();
        }

        public async Task<List<Order>> GetOrdersAsync()
        {
            return await Orders.Include(x => x.Customer).Include(x => x.ItemOrders).ThenInclude(x => x.Item).ToListAsync();
        }
    }
}
