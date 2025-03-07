namespace OrderManagement.Data
{
    public class Order
    {
        public int Id { get; set; }
        //Items, Quantity
        public Dictionary<OrderItem, int> OrderItems = new();
        public Customer Customer { get; set; } = new();
        public DateTime OrderDate { get; set; } = DateTime.Now;
    }
}
