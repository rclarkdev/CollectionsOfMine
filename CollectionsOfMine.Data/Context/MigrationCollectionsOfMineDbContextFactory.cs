using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace CollectionsOfMine.Data.Context
{
    public class MigrationCollectionsOfMineDbContextFactory : IDesignTimeDbContextFactory<CollectionsOfMineDbContext>
    {
        public CollectionsOfMineDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<CollectionsOfMineDbContext>();

            var dbContext = new CollectionsOfMineDbContext(builder.Options);

            dbContext.Database.EnsureCreated();

            DbInitializer.Initialize(dbContext);

            return new CollectionsOfMineDbContext(builder.Options);
        }
    }
}