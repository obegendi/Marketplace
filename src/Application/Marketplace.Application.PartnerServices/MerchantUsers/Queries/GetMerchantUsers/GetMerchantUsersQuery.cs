using System;
using Marketplace.API.Infrastructure;
using Marketplace.Common.Application.Queries;

namespace Marketplace.Application.MerchantServices.MerchantUsers.Queries.GetMerchantUsers
{
    public class GetMerchantUsersQuery : QueryBase<BaseListResponseModel<MerchantUserDto>>, IQuery<BaseListResponseModel<MerchantUserDto>>
    {
        public GetMerchantUsersQuery(Guid merchantCode, string search, int limit, int skip, string orderby) : base(search, skip, limit, orderby)
        {
            MerchantCode = merchantCode;
        }
        public Guid MerchantCode { get; }
    }
}
