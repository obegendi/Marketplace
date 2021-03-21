using System;
using Marketplace.Common.Application.Commands;

namespace Marketplace.Application.MerchantServices.MerchantProducts.Commands.UpdateMerchantProduct
{
    public class UpdateMerchantProductCommand : CommandBase
    {
        public UpdateMerchantProductCommand(Guid merchantCode, Guid merchantAddressCode, string productName, decimal price, decimal vat, decimal priceWithVat,
            bool isInfiniteStock, decimal? stock, bool isActive, string sku)
        {
            MerchantCode = merchantCode;
            MerchantAddressCode = merchantAddressCode;
            ProductName = productName;
            Price = price;
            Vat = vat;
            PriceWithVat = priceWithVat;
            IsInfiniteStock = isInfiniteStock;
            Stock = stock;
            IsActive = isActive;
            Sku = sku;
        }

        public Guid MerchantCode { get; }
        public Guid MerchantAddressCode { get; }
        public string ProductName { get; }
        public decimal Price { get; set; }
        public decimal Vat { get; set; }
        public decimal PriceWithVat { get; set; }
        public bool IsInfiniteStock { get; set; }
        public decimal? Stock { get; set; }
        public bool IsActive { get; set; }
        public string Sku { get; set; }
    }
}
