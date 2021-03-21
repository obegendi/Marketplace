using System;
using System.Runtime.Serialization;
using Marketplace.Common;

namespace Marketplace.Application.OrderServices.GetMerchantOrders
{
    [Serializable]
    internal class MerchantOrderNotFoundException : NotFoundException
    {
        public MerchantOrderNotFoundException()
        {
        }

        public MerchantOrderNotFoundException(string message) : base(message)
        {
        }

        public MerchantOrderNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected MerchantOrderNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
