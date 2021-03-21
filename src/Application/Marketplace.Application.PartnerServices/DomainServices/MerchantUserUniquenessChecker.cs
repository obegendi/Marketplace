using System.Threading.Tasks;
using Marketplace.Data.Repositories.Interfaces;
using Marketplace.Domain.Merchant.Rules;

namespace Marketplace.Application.MerchantServices.DomainServices
{
    public class MerchantUserUniquenessChecker : IMerchantUserUniquenessChecker
    {
        private readonly IMerchantUserRepository _accountUserRepository;

        public MerchantUserUniquenessChecker(IMerchantUserRepository accountUserRepository)
        {
            _accountUserRepository = accountUserRepository;
        }

        public async Task<bool> IsUnique(string phone)
        {
            var user = await _accountUserRepository.GetAsync(phone);
            if (user is null)
                return false;
            return true;
        }
    }
}
