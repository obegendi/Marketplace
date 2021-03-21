using System;
using System.Collections.Generic;
using Marketplace.API.Infrastructure;
using Marketplace.Common;
using Marketplace.Common.Application.Queries;

namespace Marketplace.Application.OrderServices.GetMerchantOrders
{
    public class GetMerchantOrdersQuery : QueryBase<BaseListResponseModel<OrderDto>>, IQuery<IEnumerable<OrderDto>>
    {
        public GetMerchantOrdersQuery(Guid merchantCode, string search, int skip, int limit, string orderBy) : base(search, skip, limit, orderBy)
        {
            MerchantCode = merchantCode;
        }

        public Guid MerchantCode { get; }
    }
}
