using System;
using System.Runtime.Serialization;

namespace Marketplace.Application.MerchantServices.MerchantAddresses.Commands.AddMerchantAddress
{
    [Serializable]
    internal class MerchantAddressAlreadyRegisteredException : ApplicationException
    {
        public MerchantAddressAlreadyRegisteredException()
        {
        }

        public MerchantAddressAlreadyRegisteredException(string message) : base(message)
        {
        }

        public MerchantAddressAlreadyRegisteredException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected MerchantAddressAlreadyRegisteredException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
