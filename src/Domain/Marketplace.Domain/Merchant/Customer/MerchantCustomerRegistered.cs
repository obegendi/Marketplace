using Marketplace.Domain.Seed;

namespace Marketplace.Domain.Merchant.Customer
{
    internal class MerchantCustomerRegistered : DomainEventBase
    {
        private MerchantCustomer customer;

        public MerchantCustomerRegistered(MerchantCustomer customer)
        {
            this.customer = customer;
        }
    }
}
