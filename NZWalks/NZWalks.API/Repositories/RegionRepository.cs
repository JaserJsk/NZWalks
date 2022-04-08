using Microsoft.EntityFrameworkCore;
using NZWalks.API.Models.Domain;
using NZWalks.API.Data;

namespace NZWalks.API.Repositories
{
    public class RegionRepository : IRegionRepository
    {
        #region Fields
        private readonly NZWalksDbContext context;
        #endregion

        #region Constructor
        public RegionRepository(NZWalksDbContext context)
        {
            this.context = context;
        }
        #endregion

        #region GET ALL
        public async Task<IEnumerable<Region>> GetAllAsync()
        {
            return await context.Regions.ToListAsync();
        }
        #endregion

        #region GET
        public async Task<Region> GetAsync(Guid id)
        {
            return await context.Regions.FirstOrDefaultAsync(x => x.Id == id);
        }
        #endregion

        #region ADD
        public async Task<Region> AddAsync(Region region)
        {
            region.Id = Guid.NewGuid();

            await context.AddAsync(region);
            await context.SaveChangesAsync();

            return region;
        }
        #endregion

        #region UPDATE
        public async Task<Region> UpdateAsync(Guid id, Region region)
        {
            var existingRegion = await context.Regions.FirstOrDefaultAsync(x => x.Id == id);

            if (existingRegion == null)
            {
                return null;
            }

            existingRegion.Code = region.Code;
            existingRegion.Name = region.Name;
            existingRegion.Lat = region.Lat;
            existingRegion.Long = region.Long;
            existingRegion.Population = region.Population;

            await context.SaveChangesAsync();

            return existingRegion;
        }
        #endregion

        #region DELETE
        public async Task<Region> DeleteAsync(Guid id)
        {
            var region = await context.Regions.FirstOrDefaultAsync(x => x.Id == id);

            if (region == null) 
            { 
                return null; 
            }

            // If region id is found, delete from database
            context.Regions.Remove(region);
            await context.SaveChangesAsync();

            return region;

        }
        #endregion

    }
}
