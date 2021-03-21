using System.Collections.Generic;
using Marketplace.Domain.Product;

namespace Marketplace.Application.ProductServices
{
    public class ProductDto
    {
        public string Name { get; set; }
        public string Sku { get; set; }
        public string Description { get; set; }

        public List<Image> Images { get; set; }
        public List<string> Tags { get; set; }
    }
}
