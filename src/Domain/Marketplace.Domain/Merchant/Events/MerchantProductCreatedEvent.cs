using Marketplace.Domain.Seed;

namespace Marketplace.Domain.Merchant.Events
{
    internal class MerchantProductCreatedEvent : DomainEventBase
    {
        private MerchantProduct merchantProduct;

        public MerchantProductCreatedEvent(MerchantProduct merchantProduct)
        {
            this.merchantProduct = merchantProduct;
        }
    }
}
