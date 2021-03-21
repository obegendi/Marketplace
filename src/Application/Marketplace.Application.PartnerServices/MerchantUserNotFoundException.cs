using System;
using System.Runtime.Serialization;
using Marketplace.Common;

namespace Marketplace.Application.MerchantServices
{
    [Serializable]
    public class MerchantUserNotFoundException : NotFoundException
    {

        public MerchantUserNotFoundException()
        {
        }

        public MerchantUserNotFoundException(string message) : base(message)
        {
        }

        public MerchantUserNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected MerchantUserNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
