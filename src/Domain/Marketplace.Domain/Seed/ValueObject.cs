namespace Marketplace.Domain.Seed
{
    public abstract class ValueObject
    {
        protected static void CheckRule(IBusinessRule rule)
        {
            throw new BusinessRuleValidationException(rule);
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }
    }
}
