using System;
using System.Runtime.Serialization;
using Marketplace.Common;

namespace Marketplace.Application.MerchantServices.AvailableLocations.Queries.GetAvailableLocations
{
    [Serializable]
    internal class AvailableLocationNotFoundException : NotFoundException
    {
        public AvailableLocationNotFoundException()
        {
        }

        public AvailableLocationNotFoundException(string message) : base(message)
        {
        }

        public AvailableLocationNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected AvailableLocationNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
