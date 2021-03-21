using System;

namespace Marketplace.Domain.Seed
{
    public class DomainEventBase : IDomainEvent
    {
        public DomainEventBase()
        {
            OccuredOn = DateTime.Now;
        }

        public DateTime OccuredOn { get; }
    }
}
