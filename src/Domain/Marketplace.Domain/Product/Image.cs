using Marketplace.Domain.Seed;

namespace Marketplace.Domain.Product
{
    public class Image : ValueObject
    {
        public string Name { get; set; }
        public string Url { get; set; }
    }
}