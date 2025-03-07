using System.ComponentModel.DataAnnotations.Schema;

namespace OrderManagement.Data
{
    public class OrderItem
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        // xx xxx xxx,xx
        [Column(TypeName = "decimal(8, 2)")]
        public decimal Price { get; set; }
        public List<Order> Orders { get; set; } = new();
    }
}
