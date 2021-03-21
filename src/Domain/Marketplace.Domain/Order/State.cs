using System;
using Marketplace.Domain.Seed;

namespace Marketplace.Domain.Order
{
    public class State : ValueObject
    {
        public string StateName { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
