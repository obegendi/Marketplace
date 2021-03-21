using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Marketplace.API.Infrastructure.Helper
{
    public static class ClaimExtensions
    {
        public static Guid GetMerchantCode(this HttpContext context)
        {
            var merchantCode = context?.User?.Claims.FirstOrDefault(x => x.Type == "MerchantCode");
            if (merchantCode is null)
                throw new AuthenticationException("");
            else
                return Guid.Parse(merchantCode.Value);
        }
    }
}
