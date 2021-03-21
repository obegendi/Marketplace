using System;
using Marketplace.Common.Application.Commands;

namespace Marketplace.Application.MerchantServices.MerchantAddresses.Commands.DeleteMerchantAddress
{
    public class DeleteMerchantAddressCommand : CommandBase
    {
        public DeleteMerchantAddressCommand(Guid merchantCode, Guid merchantAddressCode)
        {
            MerchantCode = merchantCode;
            MerchantAddressCode = merchantAddressCode;
        }

        public Guid MerchantCode { get; }
        public Guid MerchantAddressCode { get; }
    }

}
