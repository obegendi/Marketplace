using System.Collections.Generic;

namespace Marketplace.Application.ProductServices.Commands.Products.UpdateProduct
{
    public class UpdateProductReq
    {
        public string Name { get; set; }
        public string Barcode { get; set; }
        public string Unit { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public List<string> ImageUrls { get; set; }
        public List<string> Tags { get; set; }
    }
}
