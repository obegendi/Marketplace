using System;
using System.Runtime.Serialization;
using Marketplace.Domain.Base;

namespace Marketplace.Domain.Order.Exceptions
{
    [Serializable]
    internal class ProductQuantityMustBeGreaterThanZeroBusinessException : DomainException
    {
        public ProductQuantityMustBeGreaterThanZeroBusinessException()
        {
        }

        public ProductQuantityMustBeGreaterThanZeroBusinessException(string message) : base(message)
        {
        }
    }
}
