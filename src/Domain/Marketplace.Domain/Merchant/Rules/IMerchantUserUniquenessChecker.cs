using System.Threading.Tasks;

namespace Marketplace.Domain.Merchant.Rules
{
    public interface IMerchantUserUniquenessChecker
    {
        Task<bool> IsUnique(string phone);
    }
}
