using Marketplace.Domain.Base;
using System;
using System.Runtime.Serialization;

namespace Marketplace.Domain.Order.Exceptions
{
    [Serializable]
    internal class OrderProductListNotContainAnyProductBusinessException : DomainException
    {
        public OrderProductListNotContainAnyProductBusinessException()
        {
        }

        public OrderProductListNotContainAnyProductBusinessException(string message) : base(message)
        {
        }
    }
}
