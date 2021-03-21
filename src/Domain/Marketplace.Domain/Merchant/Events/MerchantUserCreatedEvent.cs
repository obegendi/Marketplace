using Marketplace.Domain.Seed;

namespace Marketplace.Domain.Merchant.Events
{
    internal class MerchantUserCreatedEvent : DomainEventBase
    {
        private readonly MerchantUser _merchantUser;

        public MerchantUserCreatedEvent(MerchantUser merchantUser)
        {
            _merchantUser = merchantUser;
        }
    }
}
