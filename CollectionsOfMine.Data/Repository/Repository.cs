using CollectionsOfMine.Data.Context;

namespace CollectionsOfMine.Data.Repository
{
    public class Repository<T> : RepositoryWithTypedId<T, long>, IRepository<T>
        where T : class
    {
        public Repository(CollectionsOfMineDbContext context) : base(context)
        {
        }
    }
}
