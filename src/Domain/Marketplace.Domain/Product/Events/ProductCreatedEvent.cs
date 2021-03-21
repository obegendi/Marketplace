using Marketplace.Domain.Seed;

namespace Marketplace.Domain.Product.Events
{
    internal class ProductCreatedEvent : DomainEventBase
    {
        private Product product;

        public ProductCreatedEvent(Product product)
        {
            this.product = product;
        }
    }
}
