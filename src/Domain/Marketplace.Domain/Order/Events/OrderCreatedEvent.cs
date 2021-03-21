using Marketplace.Domain.Seed;

namespace Marketplace.Domain.Order.Events
{
    internal class OrderCreatedEvent : DomainEventBase
    {
        private Order order;

        public OrderCreatedEvent(Order order)
        {
            this.order = order;
        }
    }
}
