using Marketplace.Domain.Merchant;
using Marketplace.Domain.Merchant.Customer;
using Marketplace.Domain.Order;
using Marketplace.Domain.Product;
using MongoDB.Driver;

namespace Marketplace.Data
{
    public interface IMongoDbContext
    {
        IMongoCollection<MerchantUser> MerchantUsers { get; }
        IMongoCollection<Merchant> Merchants { get; }
        IMongoCollection<MerchantAddress> MerchantAddresses { get; }
        IMongoCollection<MerchantProduct> MerchantProducts { get; }
        IMongoCollection<MerchantCustomer> MerchantCustomers { get; }
        IMongoCollection<Product> Products { get; }
        IMongoCollection<ProductTag> ProductTags { get; }
        IMongoCollection<Order> Orders { get; }
    }
}
