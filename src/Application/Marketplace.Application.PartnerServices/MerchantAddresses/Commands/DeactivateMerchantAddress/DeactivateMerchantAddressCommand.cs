using System;
using Marketplace.Common.Application.Commands;

namespace Marketplace.Application.MerchantServices.MerchantAddresses.Commands.DeactivateMerchantAddress
{
    public class DeactivateMerchantAddressCommand : CommandBase
    {
        public DeactivateMerchantAddressCommand(Guid merchantCode, Guid merchantAddressCode)
        {
            MerchantCode = merchantCode;
            MerchantAddressCode = merchantAddressCode;
        }

        public Guid MerchantCode { get; }
        public Guid MerchantAddressCode { get; }
    }
}
