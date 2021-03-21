using System.Collections.Generic;
using Marketplace.Common.Application.Commands;

namespace Marketplace.Application.ProductServices.Commands.Products.UpdateProduct
{

    public class UpdateProductCommand : CommandBase
    {
        public UpdateProductCommand(string name,
            string sku,
            string barcode,
            string unit,
            string description,
            bool isActive,
            string createdBy,
            List<string> imageUrls,
            List<string> productTags)
        {
            Name = name;
            Sku = sku;
            Barcode = barcode;
            Unit = unit;
            Description = description;
            IsActive = isActive;
            CreatedBy = createdBy;
            ImageUrls = imageUrls;
            ProductTags = productTags;
        }

        public string Name { get; }
        public string Sku { get; }
        public string Barcode { get; }
        public string Unit { get; }
        public string Description { get; }
        public bool IsActive { get; }
        public string CreatedBy { get; }
        public List<string> ImageUrls { get; }
        public List<string> ProductTags { get; }
    }

}
