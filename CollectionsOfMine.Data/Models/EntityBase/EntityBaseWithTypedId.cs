namespace CollectionsOfMine.Data.Models
{
    public abstract class EntityBaseWithTypedId<TId> : IEntityWithTypedId<TId>
    {
        public TId Id { get; protected set; }
    }
}
