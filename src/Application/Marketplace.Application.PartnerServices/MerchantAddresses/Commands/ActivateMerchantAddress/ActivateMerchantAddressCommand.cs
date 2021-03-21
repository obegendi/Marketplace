using System;
using Marketplace.Common.Application.Commands;

namespace Marketplace.Application.MerchantServices.MerchantAddresses.Commands.ActivateMerchantAddress
{
    public class ActivateMerchantAddressCommand : CommandBase
    {
        public ActivateMerchantAddressCommand(Guid merchantCode, Guid merchantAddressCode)
        {
            MerchantCode = merchantCode;
            MerchantAddressCode = merchantAddressCode;
        }

        public Guid MerchantCode { get; }
        public Guid MerchantAddressCode { get; }
    }
}
