using Marketplace.Common;
using Marketplace.Domain.Merchant;
using Marketplace.Domain.Merchant.Customer;
using Marketplace.Domain.Order;
using Marketplace.Domain.Product;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;

namespace Marketplace.Data
{

    public class MongoDbContext : IMongoDbContext
    {
        private readonly MongoClient _client;
        public IMongoDatabase _db;

        public MongoDbContext(IOptions<Appsettings> options)
        {           

            if (_client == null)
                _client = new MongoClient(options.Value.MongoDbOptions.DbConnectionString);

            if (_client != null && _db == null)
                _db = _client.GetDatabase(options.Value.MongoDbOptions.Database);

            BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.Standard));

            if (!(Products.Indexes.List().ToList().Count > 1))
            {
        
            }
        }

        public IMongoCollection<MerchantUser> MerchantUsers => _db.GetCollection<MerchantUser>("MerchantUsers");

        public IMongoCollection<Merchant> Merchants => _db.GetCollection<Merchant>("Merchants");

        public IMongoCollection<MerchantAddress> MerchantAddresses => _db.GetCollection<MerchantAddress>("MerchantAddresses");


        public IMongoCollection<Product> Products => _db.GetCollection<Product>("Products");

        public IMongoCollection<ProductTag> ProductTags => _db.GetCollection<ProductTag>("ProductTags");

        public IMongoCollection<Order> Orders => _db.GetCollection<Order>("Orders");

        public IMongoCollection<MerchantProduct> MerchantProducts => _db.GetCollection<MerchantProduct>("MerchantProducts");

        public IMongoCollection<MerchantCustomer> MerchantCustomers =>
            _db.GetCollection<MerchantCustomer>("MerchantCustomers");
    }
}
