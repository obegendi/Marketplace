using System;
using System.Runtime.Serialization;

namespace Marketplace.Application.MerchantServices
{
    [Serializable]
    internal class MerchantAddressNotFoundException : ApplicationException
    {
        public MerchantAddressNotFoundException()
        {
        }

        public MerchantAddressNotFoundException(string message) : base(message)
        {
        }

        public MerchantAddressNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected MerchantAddressNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
