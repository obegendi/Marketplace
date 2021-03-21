using System;
using System.Runtime.Serialization;
using Marketplace.Common;

namespace Marketplace.Application.MerchantServices.MerchantCustomers
{
    [Serializable]
    internal class CustomerNotFoundException : NotFoundException
    {
        public CustomerNotFoundException()
        {
        }

        public CustomerNotFoundException(string message) : base(message)
        {
        }

        public CustomerNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected CustomerNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
