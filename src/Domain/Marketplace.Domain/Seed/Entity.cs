using System.Collections.Generic;

namespace Marketplace.Domain.Seed
{
    public class Entity
    {
        private List<IDomainEvent> _domainEvents;

        public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents?.AsReadOnly();

        protected void AddDomainEvent(IDomainEvent @event)
        {
            _domainEvents ??= new List<IDomainEvent>();
            _domainEvents.Add(@event);
        }

        public void ClearDomainEvents()
        {
            _domainEvents?.Clear();
        }

        protected static void CheckRule(IBusinessRule rule)
        {
            if (rule.IsBroken())
                throw new BusinessRuleValidationException(rule);
        }
    }
}
