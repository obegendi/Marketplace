using System;
using System.Runtime.Serialization;
using Marketplace.Common;

namespace Marketplace.Application.OrderServices
{
    [Serializable]
    internal class OrderProductFoundException : NotFoundException
    {
        public OrderProductFoundException()
        {
        }

        public OrderProductFoundException(string message) : base(message)
        {
        }

        public OrderProductFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected OrderProductFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
