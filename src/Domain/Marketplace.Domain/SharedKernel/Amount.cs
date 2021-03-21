using Marketplace.Domain.Seed;

namespace Marketplace.Domain.SharedKernel
{
    public class Amount : ValueObject
    {
        public decimal Price { get; set; }
        public decimal Vat { get; set; }
        public decimal PriceWithVat { get; set; }
    }
}
