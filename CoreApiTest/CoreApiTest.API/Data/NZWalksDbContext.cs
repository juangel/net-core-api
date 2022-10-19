using CoreApiTest.API.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace CoreApiTest.API.Data
{
    public class NZWalksDbContext:DbContext
    {
        public NZWalksDbContext(DbContextOptions<NZWalksDbContext> options):base(options)
        {

        }

        public DbSet<Region> Regions { get; set; }
        public DbSet<Walk> Walks { get; set; }
        public DbSet<WalkDifficulty> WalkDifficulty { get; set; }

    }
}
