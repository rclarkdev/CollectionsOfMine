using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace CollectionsOfMine.Data.Context
{
    public class MigrationCollectionsOfMineDbContextFactory : IDesignTimeDbContextFactory<CollectionnsOfMineDbContext>
    {
        public CollectionnsOfMineDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<CollectionnsOfMineDbContext>();

            var dbContext = new CollectionnsOfMineDbContext(builder.Options);

            dbContext.Database.EnsureCreated();

            DbInitializer.Initialize(dbContext);

            return new CollectionnsOfMineDbContext(builder.Options);
        }
    }
}