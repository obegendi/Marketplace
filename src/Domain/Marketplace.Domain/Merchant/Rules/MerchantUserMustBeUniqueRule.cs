using Marketplace.Domain.Seed;

namespace Marketplace.Domain.Merchant.Rules
{
    public class MerchantUserMustBeUniqueRule : IBusinessRule
    {
        private readonly IMerchantUserUniquenessChecker _merchantUniquenessChecker;
        private readonly string _phone;

        public MerchantUserMustBeUniqueRule(IMerchantUserUniquenessChecker merchantUniquenessChecker, string phone)
        {
            _merchantUniquenessChecker = merchantUniquenessChecker;
            _phone = phone;
        }

        public string Message => "Account user already created!";

        public bool IsBroken()
        {
            return _merchantUniquenessChecker.IsUnique(_phone).Result;
        }
    }
}
