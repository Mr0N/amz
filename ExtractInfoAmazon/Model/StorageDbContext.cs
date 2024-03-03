using ExtractInfoAmazon.Model.Db;
using Microsoft.EntityFrameworkCore;

namespace ExtractInfoAmazon.Model
{
    public class StorageDbContext:DbContext
    {
        public DbSet<SaveInfoAmazon> info { set; get; }
        public StorageDbContext(DbContextOptions<StorageDbContext> config) :base(config)
        {
            Database.EnsureCreated();
        }
    }
}
