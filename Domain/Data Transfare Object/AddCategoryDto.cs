using System.ComponentModel.DataAnnotations;

namespace Domain.Data_Transfare_Object
{
    public class AddCategoryDto
    {

        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string Title { get; set; } = string.Empty;
    }
}
