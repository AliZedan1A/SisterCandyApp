using Microsoft.EntityFrameworkCore;
using ServerSide.Database;
using ServerSide.Models.DatabaseModel;

namespace ServerSide.Repository
{
    public class CatigoryRepository : Repository<CandyCategoryModel>
    {
        private readonly ApplicatonDbContext _Context;
        public CatigoryRepository(ApplicatonDbContext context) : base(context)
        {
            _Context =context;
        }

    }
}
