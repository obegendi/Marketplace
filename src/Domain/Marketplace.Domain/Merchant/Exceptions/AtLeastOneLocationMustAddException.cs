using System;
using System.Runtime.Serialization;
using Marketplace.Domain.Base;

namespace Marketplace.Domain.Merchant.Exceptions
{
    [Serializable]
    internal class AtLeastOneLocationMustAddException : DomainException
    {
        public AtLeastOneLocationMustAddException()
        {
        }

        public AtLeastOneLocationMustAddException(string message) : base(message)
        {
        }

    }
}
