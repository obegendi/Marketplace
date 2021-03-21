using System;
using Marketplace.Common.Application.Commands;

namespace Marketplace.Application.MerchantServices.MerchantProducts.Commands.CreateMerchantProduct
{
    public class CreateMerchantProductCommand : CommandBase
    {

        public CreateMerchantProductCommand(Guid merchantCode, Guid merchantAddressCode, decimal price, decimal vat, decimal priceWithVat,
            bool isInfiniteStock, decimal? stock, bool isActive, string sku)
        {
            MerchantCode = merchantCode;
            MerchantAddressCode = merchantAddressCode;
            Price = price;
            Vat = vat;
            PriceWithVat = priceWithVat;
            IsInfiniteStock = isInfiniteStock;
            Stock = stock;
            IsActive = isActive;
            Sku = sku;
        }
        public decimal Price { get; set; }
        public decimal Vat { get; set; }
        public decimal PriceWithVat { get; set; }
        public bool IsInfiniteStock { get; set; }
        public decimal? Stock { get; set; }
        public bool IsActive { get; set; }
        public string Sku { get; set; }
        public Guid MerchantAddressCode { get; set; }
        public Guid MerchantCode { get; }
    }
}
