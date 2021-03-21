using System.Collections.Generic;
using Marketplace.Common.Application.Commands;

namespace Marketplace.Application.ProductServices.Commands.Products.CreateProduct
{
    public class CreateProductCommand : CommandBase
    {
        public CreateProductCommand(
            string name,
            string barcode,
            string unit,
            string description,
            bool isActive,
            string createdBy,
            List<string> imageUrls,
            List<string> tags)
        {
            Name = name;
            Barcode = barcode;
            Unit = unit;
            Description = description;
            IsActive = isActive;
            CreatedBy = createdBy;
            ImageUrls = imageUrls;
            Tags = tags;
        }

        public string Name { get; }
        public string Barcode { get; }
        public string Unit { get; }
        public string Description { get; set; }
        public bool IsActive { get; }
        public string CreatedBy { get; }
        public List<string> ImageUrls { get; set; }
        public List<string> Tags { get; set; }
    }
}
