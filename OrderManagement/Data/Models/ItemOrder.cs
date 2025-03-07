namespace OrderManagement.Data.Models
{
    public class ItemOrder
    {
        public int ItemId { get; set; }
        public Item Item { get; set; } = new();

        public int OrderId { get; set; }
        public Order Order { get; set; } = new();

        public int Quantity { get; set; } = 1;

        public bool Removed { get; set; } = false;
    }
}
