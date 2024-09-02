using Microsoft.EntityFrameworkCore;

using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public class DifficultyRepository : IDifficultyRepository
    {
        private readonly NZWalksDbContext dbContext;
        public DifficultyRepository(NZWalksDbContext dbcontext) 
        {
            this.dbContext = dbcontext;
        }
        public async Task<List<Difficulty>> GetAllDifficultyAsync()
        {
            return await dbContext.Difficulties.ToListAsync();  
        }
    }
}
