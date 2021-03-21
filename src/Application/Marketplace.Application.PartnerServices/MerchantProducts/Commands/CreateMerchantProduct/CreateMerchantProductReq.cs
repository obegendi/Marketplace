using System;

namespace Marketplace.Application.MerchantServices.MerchantProducts.Commands.CreateMerchantProduct
{
    public class CreateMerchantProductReq
    {
        public decimal Price { get; set; }
        public decimal Vat { get; set; }
        public decimal PriceWithVat { get; set; }
        public bool IsInfiniteStock { get; set; }
        public decimal? Stock { get; set; }
        public bool IsActive { get; set; }
        public string Sku { get; set; }
        public Guid MerchantAddressCode { get; set; }
    }
}
