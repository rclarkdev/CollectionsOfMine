using System.Reflection;

namespace CollectionsOfMine.Extensions
{
    public static class FilterExtensions
    {
        public static List<T> FilterById<T>(this List<T> collection, long id)
        {
            var filteredCollection = new List<T>();
            foreach (var item in collection)
            {
                var propertyInfo = item.GetType().GetProperty("Id", BindingFlags.Public | BindingFlags.Instance);
                var propertyValue = propertyInfo.GetValue(item, null);
                if ((long)propertyValue == id) filteredCollection.Add(item);
            }
            return filteredCollection;
        }
    }
}
