using System;
using System.Runtime.Serialization;
using Marketplace.Domain.Base;

namespace Marketplace.Domain.Merchant.Exceptions
{
    [Serializable]
    internal class ActiveAddressNameNotfoundException : DomainException
    {
        public ActiveAddressNameNotfoundException()
        {
        }

        public ActiveAddressNameNotfoundException(string message) : base(message)
        {
        }
    }
}
