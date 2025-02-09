using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ServerSide.Models.DatabaseModel
{
    [Index(nameof(Title), IsUnique = true)]
    public class CandyCategoryModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string Title { get; set; } = string.Empty;
        [JsonIgnore]
        public virtual List<CandyModel> Candies { get; set; } = new List<CandyModel>();
    }
}