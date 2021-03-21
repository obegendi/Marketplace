using System;
using System.Runtime.Serialization;
using Marketplace.Domain.Base;

namespace Marketplace.Domain.Merchant.Customer
{
    [Serializable]
    public class CustomerAddressNotFound : DomainException
    {
        public CustomerAddressNotFound() 
        {
            
        }
    }
}
