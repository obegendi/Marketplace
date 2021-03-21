using System;
using System.Runtime.Serialization;
using Marketplace.Domain.Base;

namespace Marketplace.Domain.Order.Exceptions
{
    [Serializable]
    internal class OrderStatesNotFoundBusinessException : DomainException
    {
        public OrderStatesNotFoundBusinessException()
        {
        }

        public OrderStatesNotFoundBusinessException(string message) : base(message)
        {
        }
    }
}
