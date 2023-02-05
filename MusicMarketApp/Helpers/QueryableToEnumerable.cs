using MusicMarketKursach.Models.Entitys;
using System.Collections.Generic;
using System.Linq;

namespace MusicMarketKursach.Helpers
{
    public static class QueryableToEnumerable<T> where T : Entity
    {
        public static IEnumerable<T> Convert(IQueryable<T> values)
        {
            IEnumerable<T> result = new List<T>();
            foreach(var item in values)
            {
                result = result.Append(item);
            }
            return result;
        }
    }
}
