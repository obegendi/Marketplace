using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Marketplace.Data.Repositories.Interfaces;
using Marketplace.Domain.Merchant.Rules;
using Marketplace.Domain.SharedKernel;

namespace Marketplace.Application.MerchantServices.DomainServices
{
    public class LocationUniquenessChecker : ILocationUniquenessChecker
    {
        private readonly IMerchantAddressRepository _merchantAddressRepository;

        public LocationUniquenessChecker(IMerchantAddressRepository merchantAddressRepository)
        {
            _merchantAddressRepository = merchantAddressRepository;
        }

        public async Task<bool> IsUnique(Guid merchantCode, Guid merchantAddressCode, List<Location> locations)
        {
            var availableLocation = await _merchantAddressRepository.GetAsync(merchantCode, merchantAddressCode);
            foreach (var location in availableLocation.AvailableLocations)
                if (locations.Any(x => x.Equals(location)))
                    return false;
            return true;
        }
    }
}
