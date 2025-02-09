using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ServerSide.Models.DatabaseModel
{
    [Index(nameof(Name), nameof(CategoryId), IsUnique = true)]
    public class CandyModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string Image { get; set; } = string.Empty;

        [Required]
        [Column(TypeName = "decimal(18,2)")] 
        public decimal Price { get; set; }

        [Required]
        public int CategoryId { get; set; } 


        [ForeignKey(nameof(CategoryId))] 
        public virtual CandyCategoryModel Category { get; set; }
    }
}