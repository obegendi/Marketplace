using System;
using System.Runtime.Serialization;

namespace Marketplace.Application.MerchantServices.MerchantUsers.Commands.ForgotPassword
{
    [Serializable]
    internal class MerchantUserNotNotFound : Exception
    {
        public MerchantUserNotNotFound()
        {
        }

        public MerchantUserNotNotFound(string message) : base(message)
        {
        }

        public MerchantUserNotNotFound(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected MerchantUserNotNotFound(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
