using Marketplace.Domain.Seed;
using System;

namespace Marketplace.Domain.SharedKernel
{
    public class Address : ValueObject
    {
        public Guid Code { get; set; }
        public string Name { get; set; }
        public string FullAddress { get; set; }
        public string City { get; set; }
        public string Town { get; set; }
        public string District { get; set; }
    }
}
