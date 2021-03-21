using System;
using System.Runtime.Serialization;
using Marketplace.Common;

namespace Marketplace.Application.OrderServices
{
    [Serializable]
    internal class OrderNotFoundException : NotFoundException
    {
        public OrderNotFoundException()
        {
        }

        public OrderNotFoundException(string message) : base(message)
        {
        }

        public OrderNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected OrderNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
