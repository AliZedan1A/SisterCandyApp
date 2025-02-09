

using Domain.Database;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.DataModels
{
    public class OrderData
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public string Note { get; set; }
        public string PhoneNumber { get; set; }
        public List<OrderItem> Items { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime DeliveryDate { get; set; }
        public string Status { get; set; }
        public decimal TotalPrice => Items.Sum(i => i.Price * i.Quantity);
    }

    public class OrderItem
    {
        [Key]
        public int id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }

        public int Quantity { get; set; }
    }

}

