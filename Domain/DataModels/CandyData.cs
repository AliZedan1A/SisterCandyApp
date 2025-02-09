

namespace Domain.DataModels
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    namespace ServerSide.Models.DatabaseModel
    {
        public class CandyData
        {
        
            public int Id { get; set; }

        
           
            public string Name { get; set; }

   
            public string Image { get; set; } 
        
           
            public decimal Price { get; set; }
            public string Category { get; set; }
         
            public int CategoryId { get; set; }
          

        }
    }
}
