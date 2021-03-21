using MongoDB.Driver;

namespace Marketplace.Common.Extensions
{
    public static class QueryExtensions
    {
        /// <summary>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="string">Supported : asc, desc</param>
        /// <param name="sortBy">Default is Name</param>
        /// <returns></returns>
        public static SortDefinition<T> ToSortDefinition<T>(this string @string, string sortBy = null) where T : class
        {
            if (sortBy is null)
                sortBy = "Name";
            if (@string == "asc")
                return Builders<T>.Sort.Ascending(sortBy);
            if (@string == "desc")
                return Builders<T>.Sort.Descending(sortBy);
            return Builders<T>.Sort.Ascending(sortBy);
        }
    }
}
