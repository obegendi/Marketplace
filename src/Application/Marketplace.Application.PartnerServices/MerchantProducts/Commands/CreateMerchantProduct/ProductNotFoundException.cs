using System;
using System.Runtime.Serialization;

namespace Marketplace.Application.MerchantServices.MerchantProducts.Commands.CreateMerchantProduct
{
    [Serializable]
    internal class ProductNotFoundException : ApplicationException
    {
        public ProductNotFoundException()
        {
        }

        public ProductNotFoundException(string message) : base(message)
        {
        }

        public ProductNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ProductNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
