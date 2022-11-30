using Microsoft.EntityFrameworkCore;
using CollectionsOfMine.Data.Context;
using CollectionsOfMine.Data.Models;
using CollectionsOfMine.Data.Repository;
using Attribute = CollectionsOfMine.Data.Models.Attribute;
using File = CollectionsOfMine.Data.Models.File;

namespace CollectionsOfMine.Services
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CollectionsOfMineDbContext _dbContext;

        #region Repositories

        public IRepository<Item> ItemRepository =>
            new Repository<Item>(_dbContext);

        public IRepository<Collection> CollectionRepository =>
            new Repository<Collection>(_dbContext);

        public IRepository<Area> AreaRepository =>
            new Repository<Area>(_dbContext);

        public IRepository<Attribute> AttributeRepository =>
            new Repository<Attribute>(_dbContext);

        public IRepository<File> FileRepository =>
            new Repository<File>(_dbContext);

        public IRepository<User> UserRepository =>
            new Repository<User>(_dbContext);

        public IRepository<ContentType> ContentTypeRepository =>
            new Repository<ContentType>(_dbContext);

        #endregion

        public UnitOfWork()
        {
            _dbContext = new CollectionsOfMineDbContext();
        }
        public async Task CommitAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public async Task DisposeAsync()
        {
            await _dbContext.DisposeAsync();
        }

        public async Task RejectChangesAsync()
        {
            foreach (var entry in _dbContext.ChangeTracker.Entries()
                .Where(e => e.State != EntityState.Unchanged))
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.State = EntityState.Detached;
                        break;
                    case EntityState.Modified:
                    case EntityState.Deleted:
                        await entry.ReloadAsync();
                        break;
                }
            }
        }
    }
}
