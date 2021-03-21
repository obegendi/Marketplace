using Marketplace.Domain.Base;
using System;

namespace Marketplace.Domain.Seed
{
    [Serializable]
    internal class BusinessRuleValidationException : DomainException
    {
        public BusinessRuleValidationException()
        {
        }

        public BusinessRuleValidationException(string message) : base(message)
        {
        }

        public BusinessRuleValidationException(IBusinessRule businessRule) : base(businessRule.Message)
        {
            BusinessRule = businessRule;
        }

        public IBusinessRule BusinessRule { get; }

        public override string ToString()
        {
            return $"{BusinessRule.GetType().FullName}: {BusinessRule.Message}";
        }
    }
}
