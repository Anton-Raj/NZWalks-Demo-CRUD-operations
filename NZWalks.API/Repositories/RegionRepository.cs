using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;

namespace NZWalks.API.Repositories
{
    public class RegionRepository : IRegionRepository
    {
        private readonly NZWalksDbContext dbContext;
        public RegionRepository(NZWalksDbContext dbcontext) 
        {
            this.dbContext= dbcontext;
        }

        //Implement the interface

        public async Task<List<Region>> GetAllAsync(string? filterOn = null, string? filterQuery = null, string? sortBy = null, bool isAscending = true, int pageNumber =1, int pageSize=1000)
        {
            //return await dbContext.Regions.ToListAsync();

            var region = dbContext.Regions.AsQueryable();

            //Filtering
            if (string.IsNullOrWhiteSpace(filterOn) == false && string.IsNullOrWhiteSpace(filterQuery) == false) 
            {
                if(filterOn.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    region = region.Where(x => x.Name.Contains(filterQuery));
                }
            }

            //Sorting
            if(string.IsNullOrWhiteSpace(sortBy)==false)
            {
                if(sortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    region = isAscending ? region.OrderBy(x => x.Name) : region.OrderByDescending(x => x.Name);
                }
                else if(sortBy.Equals("Code", StringComparison.OrdinalIgnoreCase))
                {
                    region = isAscending ? region.OrderBy(x =>x.Code) : region.OrderByDescending(x => x.Code); 
                }
            }

            //Pagination
            var skipResults = (pageNumber - 1) * pageSize;
            return await region.Skip(skipResults).Take(pageSize).ToListAsync();

            //return await region.ToListAsync();
        }

        public async Task<Region?> GetByIdAsync(Guid id)
        {
            return await dbContext.Regions.FirstOrDefaultAsync(x => x.Id ==id);
        }

        public async Task<Region> CreateAsync(Region region)
        {
            await dbContext.Regions.AddAsync(region);
            await dbContext.SaveChangesAsync();
            return region;
        }
        
        //public Region CreateAsync(CreateRegionRequestDto region)
        //{
        //    var region_info = new Region { Code = region.Code, Name = region.Name, RegionImageUrl = region.RegionImageUrl };
        //     dbContext.Regions.Add(region_info);
        //     dbContext.SaveChanges();
        //    return region_info;

        //}

        public async Task<Region?> UpdateAsync(Guid id, Region region)
        {
            var existingRegion = await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);

            if(existingRegion == null)
            {
                return null;
            }
            //Update the values
            existingRegion.Code = region.Code;
            existingRegion.Name = region.Name;
            existingRegion.RegionImageUrl = region.RegionImageUrl;

            await dbContext.SaveChangesAsync();

            return existingRegion;

        }

        public async Task<Region?> DeleteAsync(Guid id)
        {
            var existingRegion = await dbContext.Regions.FirstOrDefaultAsync(x=>x.Id==id);

            if (existingRegion == null)
            {
                return null;
            }

            dbContext.Regions.Remove(existingRegion);
            await dbContext.SaveChangesAsync();

            return existingRegion;
        }
    }
}
