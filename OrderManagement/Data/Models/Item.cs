﻿namespace OrderManagement.Data.Models
{
    public class Item
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public List<ItemOrder> ItemOrders { get; set; } = new();
    }
}
