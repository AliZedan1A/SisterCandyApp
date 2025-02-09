using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DataModels
{
    public class CategoryData
    {
        public int Id { get; set; }
        [StringLength(50, MinimumLength = 3)]
        public string Title { get; set; }

    }
}
