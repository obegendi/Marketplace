using System;
using System.Runtime.Serialization;
using Marketplace.Common;

namespace Marketplace.Application.OrderServices
{
    [Serializable]
    internal class ProductNotFoundException : NotFoundException
    {
        public ProductNotFoundException()
        {
        }

        public ProductNotFoundException(string message) : base(message)
        {
        }

        public ProductNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ProductNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
