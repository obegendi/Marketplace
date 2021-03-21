using Marketplace.Domain.Merchant.Events;
using Marketplace.Domain.Merchant.Rules;
using Marketplace.Domain.Product;
using Marketplace.Domain.Seed;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace Marketplace.Domain.Merchant
{
    [BsonIgnoreExtraElements]
    public class MerchantProduct : Entity
    {

        public MerchantProduct()
        {

        }

        private MerchantProduct(
            Guid merchantCode,
            string name,
            string sku,
            string barcode,
            decimal price,
            decimal vat,
            decimal priceWithVat,
            bool isActive,
            string description,
            decimal? stock,
            bool isInfiniteStock,
            Guid merchantAddressCode,
            List<Image> productImages,
            List<string> productTags)
        {
            MerchantCode = merchantCode;
            Name = name;
            Sku = sku;
            Barcode = barcode;
            Price = price;
            Vat = vat;
            PriceWithVat = priceWithVat;
            IsActive = isActive;
            Description = description;
            Stock = stock;
            IsInfiniteStock = isInfiniteStock;
            MerchantAddressCode = merchantAddressCode;
            ProductImageUrls = productImages;
            ProductTags = productTags;

            AddDomainEvent(new MerchantProductCreatedEvent(this));
        }
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public Guid MerchantCode { get; set; }
        public Guid MerchantAddressCode { get; set; }
        public string Name { get; set; }
        public string Sku { get; set; }
        public string Barcode { get; set; }
        public decimal Price { get; set; }
        public decimal Vat { get; set; }
        public decimal PriceWithVat { get; set; }
        public decimal? Stock { get; set; }
        public bool IsInfiniteStock { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public List<Image> ProductImageUrls { get; set; }
        public List<string> ProductTags { get; set; }

        public static MerchantProduct AddCreated(
            Guid merchantCode,
            string name,
            string sku,
            string barcode,
            decimal price,
            decimal vat,
            decimal priceWithVat,
            bool isActive,
            string description,
            decimal? stock,
            bool isInfiniteStock,
            Guid merchantAddressCode,
            List<Image> productImages,
            List<string> ProductTags)
        {
            CheckRule(new ProductVatMustBeConsistentRule(price, priceWithVat));
            var merchantProduct = new MerchantProduct(
                merchantCode,
                name,
                sku,
                barcode,
                price,
                vat,
                priceWithVat,
                isActive,
                description,
                stock,
                isInfiniteStock,
                merchantAddressCode,
                productImages,
                ProductTags);

            return merchantProduct;
        }
    }
}
