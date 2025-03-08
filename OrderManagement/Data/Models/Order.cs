using System.ComponentModel.DataAnnotations.Schema;

namespace OrderManagement.Data.Models
{
    public class Order
    {
        public int Id { get; set; }
        public List<ItemOrder> ItemOrders { get; set; } = new();
        public Customer Customer { get; set; } = new();
        public DateTime OrderDate { get; set; } = DateTime.Now;
        public bool Paid { get; set; }
        public bool Removed { get; set; } = false;
        [NotMapped]
        public bool ShowDetails { get; set; } = false;

        public decimal GetTotalDueAmount()
        {
            return ItemOrders.Sum(x => x.Item.Price * x.Quantity); 
        }
    }
}
