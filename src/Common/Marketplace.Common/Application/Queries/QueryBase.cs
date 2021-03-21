namespace Marketplace.Common.Application.Queries
{
    public abstract class QueryBase<TResult> : IQuery<TResult>
    {
        public string Search { get; }
        public int Skip { get; }
        public int Limit { get; }
        public string OrderBy { get; }
        protected QueryBase(string search, int skip, int limit, string orderBy)
        {
            Search = search;
            Skip = skip;
            Limit = limit;
            OrderBy = orderBy;

            if (limit > 10 || limit == default(int))
                this.Limit = 10;
            this.Limit += 1;
        }
    }
}
