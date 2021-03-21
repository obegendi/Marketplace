using System;

namespace Marketplace.Domain.Seed
{
    public interface IDomainEvent
    {
        DateTime OccuredOn { get; }
    }
}
