using System;
using System.Collections.Generic;
using Marketplace.Domain.Seed;

namespace Marketplace.Domain.Merchant.Rules
{
    public class MerchantAvailableLocationMustBeUniqueRule : IBusinessRule
    {
        private readonly List<SharedKernel.Location> _locations;
        private readonly ILocationUniquenessChecker _locationUniquenessChecker;
        private readonly Guid _merchantAddressCode;
        private readonly Guid _merchantCode;

        public MerchantAvailableLocationMustBeUniqueRule(ILocationUniquenessChecker locationUniquenessChecker, Guid merchantCode, Guid merchantAddressCode,
            List<SharedKernel.Location> locations)
        {
            _locationUniquenessChecker = locationUniquenessChecker;
            _merchantCode = merchantCode;
            _merchantAddressCode = merchantAddressCode;
            _locations = locations;
        }

        public string Message => "Merchant location must be unique!";

        public bool IsBroken()
        {
            return _locationUniquenessChecker.IsUnique(_merchantCode, _merchantAddressCode, _locations).Result;
        }
    }
}
