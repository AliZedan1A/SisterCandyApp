using Domain.DataModels.ServerSide.Models.DatabaseModel;
using Microsoft.EntityFrameworkCore;
using ServerSide.Database;
using ServerSide.Models.DatabaseModel;

namespace ServerSide.Repository
{
    public class CandyRepository : Repository<CandyModel>
    {
        private readonly ApplicatonDbContext _context;

        public CandyRepository(ApplicatonDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<List<CandyData>> GetCandies()
        {
            return await _context.Candy
                .Select(c => new CandyData
                {
                    Id = c.Id,
                    Name = c.Name,
                    Image = c.Image,
                    Price = c.Price,
                    Category = c.Category.Title
                    ,CategoryId = c.CategoryId,
                })
                .ToListAsync();
        }
        public async Task<List<CandyData>> GetCandiesByCategoryIdAsync(int categoryId)
        {
            return await _context.Candy
                .Where(c => c.CategoryId == categoryId)
                .Select(c => new CandyData
                {
                    Id = c.Id,
                    Name = c.Name,
                    Image = c.Image,
                    Price = c.Price,
                    Category = c.Category.Title
                    ,CategoryId = categoryId
                })
                .ToListAsync();
        }

    }
}
