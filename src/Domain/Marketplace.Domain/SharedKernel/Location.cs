using Marketplace.Domain.Seed;

namespace Marketplace.Domain.SharedKernel
{
    public class Location : ValueObject
    {
        public string City { get; set; }
        public string Town { get; set; }
        public string District { get; set; }
        public bool Status { get; set; }
    }
}
