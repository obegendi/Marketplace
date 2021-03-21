using System.Collections.Generic;
using System.Linq;

namespace Marketplace.API.Infrastructure
{
    public class BaseListResponseModel<T> where T : class
    {
        public BaseListResponseModel(int limit, IEnumerable<T> model)
        {
            if (limit > model.Count())
                IsLastPage = true;
            else
                IsLastPage = false;
            Records = model.Take(limit - 1);
        }

        public bool IsLastPage { get; }
        public IEnumerable<T> Records { get; }
    }
}
