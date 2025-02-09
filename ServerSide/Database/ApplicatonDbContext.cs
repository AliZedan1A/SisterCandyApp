using Domain.Database;
using Microsoft.EntityFrameworkCore;
using ServerSide.Models.DatabaseModel;

namespace ServerSide.Database
{
    public class ApplicatonDbContext : DbContext
    {
        public DbSet<CandyModel> Candy { get; set; }
        public DbSet<CandyCategoryModel> CandyCategory { get; set; }
        public DbSet<OrderDbModel> Orders { get; set; }
        public ApplicatonDbContext(DbContextOptions options) : base(options)
        {
            
        }

        
    }
}
