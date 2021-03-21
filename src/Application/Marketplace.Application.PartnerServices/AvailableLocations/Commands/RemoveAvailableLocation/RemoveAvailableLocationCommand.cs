using System;
using System.Collections.Generic;
using Marketplace.Common.Application.Commands;
using Marketplace.Domain.SharedKernel;

namespace Marketplace.Application.MerchantServices.AvailableLocations.Commands.RemoveAvailableLocation
{
    public class RemoveAvailableLocationCommand : CommandBase
    {
        public RemoveAvailableLocationCommand(Guid merchantCode, Guid merchantAddressCode, List<Location> locations)
        {
            MerchantCode = merchantCode;
            MerchantAddressCode = merchantAddressCode;
            Locations = locations;

        }
        public Guid MerchantCode { get; set; }
        public Guid MerchantAddressCode { get; set; }
        public List<Location> Locations { get; set; }
    }

}
