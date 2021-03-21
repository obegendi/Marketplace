using System;
using System.Collections.Generic;
using System.Text;
using Marketplace.Common.Application.Commands;

namespace Marketplace.Application.MerchantServices.MerchantProducts.Commands.DeleteMerchantProduct
{
    public class DeleteMerchantProductCommand : CommandBase<bool>
    {
        public Guid MerchantCode { get; set; }
        public Guid MerchantAddressCode { get; set; }
        public string Sku { get; set; }

        public DeleteMerchantProductCommand(Guid merchantCode, Guid merchantAddressCode, string sku)
        {
            MerchantCode = merchantCode;
            MerchantAddressCode = merchantAddressCode;
            Sku = sku;
        }
    }
}
