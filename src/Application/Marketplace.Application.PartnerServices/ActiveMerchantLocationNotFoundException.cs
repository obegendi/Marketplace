using System;
using System.Runtime.Serialization;

namespace Marketplace.Application.MerchantServices
{
    [Serializable]
    internal class ActiveMerchantLocationNotFoundException : ApplicationException
    {
        public ActiveMerchantLocationNotFoundException()
        {
        }

        public ActiveMerchantLocationNotFoundException(string message) : base(message)
        {
        }

        public ActiveMerchantLocationNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ActiveMerchantLocationNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
