using System;
using Marketplace.Common.Application.Commands;

namespace Marketplace.Application.MerchantServices.MerchantProducts.Commands.UpdateMerchantProduct
{
    public class UpdateStockMerchantProductCommand : CommandBase
    {
        public UpdateStockMerchantProductCommand(Guid merchantCode, Guid merchantAddressCode, string sku, decimal stock)
        {
            MerchantCode = merchantCode;
            MerchantAddressCode = merchantAddressCode;
            Sku = sku;
            Stock = stock;
        }

        public Guid MerchantCode { get; }
        public Guid MerchantAddressCode { get; }
        public string Sku { get; }
        public decimal Stock { get; }
    }
}
