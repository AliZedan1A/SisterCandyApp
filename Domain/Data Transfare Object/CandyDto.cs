using System.ComponentModel.DataAnnotations;

namespace Domain.Data_Transfare_Object
{
    public class CandyDto
    {
      
        [StringLength(50, MinimumLength = 3)]
        public string Name { get; set; } = string.Empty;

       
        public string Image { get; set; } = string.Empty;

     
        [Range(0.01, 1000)]
        public decimal Price { get; set; }

     
        public int CategoryId { get; set; }
    }
}
