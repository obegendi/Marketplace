using Marketplace.Domain.Seed;

namespace Marketplace.Domain.Merchant.Events
{
    internal class MerchantLocationCreatedEvent : DomainEventBase
    {
        private string merchantName;

        public MerchantLocationCreatedEvent(string merchantName)
        {
            this.merchantName = merchantName;
        }
    }
}
