using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Marketplace.Domain.Merchant.Rules
{
    public interface ILocationUniquenessChecker
    {
        Task<bool> IsUnique(Guid merchantCode, Guid merchantAddressCode, List<SharedKernel.Location> locations);
    }
}
