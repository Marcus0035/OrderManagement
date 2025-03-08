﻿using Microsoft.EntityFrameworkCore;
using OrderManagement.Data.Models;

namespace OrderManagement.Data
{
    public class OrderManagementContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<ItemOrder> ItemOrders { get; set; }
        public DbSet<UserSettings> UserSettings { get; set; }

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
        //User
        public async Task<bool> ExistingUsername(string userName)
        {
            var userSettings = await UserSettings.FirstOrDefaultAsync(x => x.Username == userName);
            return userSettings != null;
        }
        public async Task<string> GetHashedPasswordForUser(string username)
        {
            var user = await UserSettings.FirstOrDefaultAsync(x => x.Username == username);
            return user?.HashedPassword ?? string.Empty;
        }

        //Customer
        public async Task<List<Customer>> GetCustomersAsync()
        {
            return await Customers.Where(x => !x.Removed).ToListAsync();
        }
        public async Task PostCustomerAsync(Customer customer)
        {
            await Customers.AddAsync(customer);
            await SaveChangesAsync();
        }
        public async Task PutCustomerAsync(Customer customer)
        {
            var existingCustomer = Customers.First(x => x.Id == customer.Id);
            existingCustomer = customer;
            await SaveChangesAsync();

        }
        public async Task DeleteCustomersAsync(IEnumerable<Customer> customers)
        {
            foreach (var customer in customers)
            {
                customer.Removed = true;
            };

            await SaveChangesAsync();
        }

        //Order
        public async Task<List<Order>> GetOrdersAsync()
        {
            return await Orders.Where(x => !x.Removed).Include(x => x.Customer).Include(x => x.ItemOrders).ThenInclude(x => x.Item).ToListAsync();
        }
        public async Task<Order> GetOrderAsync(int Id)
        {
            return await Orders.Include(x => x.Customer).Include(x => x.ItemOrders).ThenInclude(x => x.Item).FirstAsync(x => x.Id == Id);

        }
        public async Task PostOrderAsync(Order order)
        {
            await Orders.AddAsync(order);
            await SaveChangesAsync();
        }
        public async Task DeleteOrderAsync(IEnumerable<Order> orders)
        {
            foreach (var order in orders)
            {
                order.Removed = true;
            };
            await SaveChangesAsync();
        }
        public async Task ChangePaymentStatusAsync(IEnumerable<Order> orders)
        {
            foreach (var order in orders)
            {
                order.Paid = !order.Paid;
            };
            await SaveChangesAsync();
        }

        //ItemOrder
        public async Task<List<ItemOrder>> GetItemOrdersAsync()
        {
            return await ItemOrders.Where(x => !x.Removed).Include(x => x.Item).Include(x => x.Order).ToListAsync();
        }

        //Item
        public async Task<List<Item>> GetItemsAsync()
        {
            return await Items.Where(x => !x.Removed).ToListAsync();
        }
        public async Task PostItemAsync(Item item)
        {
            await Items.AddAsync(item);
            await SaveChangesAsync();
        }
        public async Task PutItemAsync(Item item)
        {
            Items.Update(item);
            await SaveChangesAsync();
        }
        public async Task DeleteItemsAsync(IEnumerable<Item> items)
        {
            foreach (var item in items)
            {
                item.Removed = true;
            };

            await SaveChangesAsync();
        }

    }
}
