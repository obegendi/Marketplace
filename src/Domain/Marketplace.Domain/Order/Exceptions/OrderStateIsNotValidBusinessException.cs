using System;
using Marketplace.Domain.Base;

namespace Marketplace.Domain.Order.Exceptions
{
    [Serializable]
    internal class OrderStateIsNotValidBusinessException : DomainException
    {
        public OrderStateIsNotValidBusinessException()
        {
        }

        public OrderStateIsNotValidBusinessException(string message) : base(message)
        {
        }
    }
}
