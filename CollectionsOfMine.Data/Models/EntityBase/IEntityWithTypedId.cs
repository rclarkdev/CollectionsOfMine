namespace CollectionsOfMine.Data.Models
{
    public interface IEntityWithTypedId<out TId>
    {
        TId Id { get; }
    }
}
