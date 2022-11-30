using CollectionsOfMine.Data.Models;
using CollectionsOfMine.Data.Repository;
using Attribute = CollectionsOfMine.Data.Models.Attribute;
using File = CollectionsOfMine.Data.Models.File;

namespace CollectionsOfMine.Services
{
    public interface IUnitOfWork
    {
        IRepository<Item> ItemRepository { get; }
        IRepository<Collection> CollectionRepository { get; }
        IRepository<Area> AreaRepository { get; }
        IRepository<Attribute> AttributeRepository { get; }
        IRepository<File> FileRepository { get; }
        IRepository<User> UserRepository { get; }
        IRepository<ContentType> ContentTypeRepository { get; }

        /// <summary>
        /// Commits all changes
        /// </summary>
        Task CommitAsync();
        /// <summary>
        /// Discards all changes that has not been commited
        /// </summary>
        Task RejectChangesAsync();
        Task DisposeAsync();
    }
}
