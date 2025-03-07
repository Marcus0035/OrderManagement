namespace OrderManagement.Data.Models
{
    public class Item
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public List<ItemOrder> ItemOrders { get; set; } = new();
        public bool Removed { get; set; } = false;

        public Item()
        {
        }
        public Item(string name, decimal price)
        {
            Name = name;
            Price = price;
        }
    }
}
