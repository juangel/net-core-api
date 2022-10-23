using CoreApiTest.API.Data;
using CoreApiTest.API.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace CoreApiTest.API.Repositories
{
    public class WalkDifficultyRepository : IWalkDifficultyRepository
    {
        private readonly NZWalksDbContext nZWalksDbContext;

        public WalkDifficultyRepository(NZWalksDbContext nZWalksDbContext)
        {
            this.nZWalksDbContext = nZWalksDbContext;
        }

        public async Task<WalkDifficulty> AddAsync(WalkDifficulty walkDifficulty)
        {
            walkDifficulty.Id = Guid.NewGuid();
            await nZWalksDbContext.WalkDifficulty.AddAsync(walkDifficulty);
            await nZWalksDbContext.SaveChangesAsync();
            return walkDifficulty;
        }

        public async Task<WalkDifficulty> DeleteAsync(Guid id)
        {
            var exisitingWalkDifficulty = await nZWalksDbContext.WalkDifficulty.FindAsync(id);
            if (exisitingWalkDifficulty != null)
            {
                nZWalksDbContext.WalkDifficulty.Remove(exisitingWalkDifficulty);
                await nZWalksDbContext.SaveChangesAsync();
                return exisitingWalkDifficulty;
            }
            return null;
        }

        public async Task<IEnumerable<WalkDifficulty>> GetAllAsync()
        {
            return await nZWalksDbContext.WalkDifficulty.ToListAsync();
        }

        public async Task<WalkDifficulty> GetAsync(Guid id)
        {
            var exisitingWalkDifficulty = await nZWalksDbContext.WalkDifficulty.FindAsync(id);
            if (exisitingWalkDifficulty != null)
            {
                return exisitingWalkDifficulty;
            }
            return null;
        }

        public async Task<WalkDifficulty> UpdateAsync(Guid id, WalkDifficulty walkDifficulty)
        {
            var exisitingWalkDifficulty = await nZWalksDbContext.WalkDifficulty.FindAsync(id);

            if (exisitingWalkDifficulty == null)
                return null;

            exisitingWalkDifficulty.Code = walkDifficulty.Code;
            await nZWalksDbContext.SaveChangesAsync();
            return exisitingWalkDifficulty;
        }
    }
}
