namespace OrderManagement.Data
{
    public class Order
    {
        public int Id { get; set; }
        //Items, Quantity
        public Dictionary<OrderItem, int> OrderItems = new();
        public Customer Customer { get; set; } = new();
        public DateOnly OrderDate { get; set; } = DateOnly.FromDateTime(DateTime.Now);
    }
}
