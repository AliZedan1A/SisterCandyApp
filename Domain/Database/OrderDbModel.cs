
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;


namespace Domain.Database
{
    public class OrderDbModel
    {
        [Key]
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public string Note { get; set; }

        public string PhoneNumber { get; set; }
        [JsonIgnore]
        public List<OrderDbItem> Items { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime DeliveryDate { get; set; }
        public string Status { get; set; }

        public decimal TotalPrice => Items.Sum(i => i.Price * i.Quantity);

    }
    public class OrderDbItem
    {
        [Key]
        public int id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        [Required]
        public int OrderDbId { get; set; }

        [ForeignKey(nameof(OrderDbId))]
        public virtual OrderDbModel OrderDb { get; set; }
        public int Quantity { get; set; }
    }

}
