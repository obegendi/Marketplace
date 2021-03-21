using System;
using System.Collections.Generic;
using Marketplace.Common.Application.Commands;
using Marketplace.Domain.SharedKernel;

namespace Marketplace.Application.MerchantServices.AvailableLocations.Commands.ActivateAvailableLocation
{
    public class ActivateAvailableLocationCommand : CommandBase
    {
        public ActivateAvailableLocationCommand(Guid merchantCode, Guid merchantAddressCode, List<Location> locations)
        {
            MerchantCode = merchantCode;
            MerchantAddressCode = merchantAddressCode;
            Locations = locations;
        }

        public Guid MerchantCode { get; }
        public Guid MerchantAddressCode { get; }
        public List<Location> Locations { get; set; }
    }
}
