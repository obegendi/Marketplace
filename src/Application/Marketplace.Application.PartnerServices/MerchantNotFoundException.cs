using System;
using System.Runtime.Serialization;

namespace Marketplace.Application.MerchantServices
{
    [Serializable]
    internal class MerchantNotFoundException : ApplicationException
    {
        public MerchantNotFoundException()
        {
        }

        public MerchantNotFoundException(string message) : base(message)
        {
        }

        public MerchantNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected MerchantNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
