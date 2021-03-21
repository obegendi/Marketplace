using Marketplace.Domain.Seed;

namespace Marketplace.Domain.Merchant.Events
{
    internal class MerchantRegisteredEvent : DomainEventBase
    {
        private string name;

        public MerchantRegisteredEvent(string name)
        {
            this.name = name;
        }
    }
}
