using System;
using Marketplace.Common.Application.Queries;

namespace Marketplace.Application.MerchantServices.MerchantUsers.Queries.GetMerchantUser
{
    public class GetMerchantUserQuery : IQuery<MerchantUserDto>
    {
        public GetMerchantUserQuery(Guid merchantCode, string phone)
        {
            MerchantCode = merchantCode;
            Phone = phone;
        }

        public Guid MerchantCode { get; }
        public string Phone { get; }
    }
}
