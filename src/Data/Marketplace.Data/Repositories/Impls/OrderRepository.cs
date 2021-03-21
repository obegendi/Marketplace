using Marketplace.Common.Extensions;
using Marketplace.Data.Repositories.Interfaces;
using Marketplace.Domain.Order;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Marketplace.Data.Repositories.Impls
{
    public class OrderRepository : IOrderRepository
    {
        private readonly IMongoDbContext _mongoDbContext;

        public OrderRepository(IMongoDbContext mongoDbContext)
        {
            _mongoDbContext = mongoDbContext;
        }

        public async Task CreateAsync(Order order)
        {
            await _mongoDbContext.Orders.InsertOneAsync(order);
        }

        public async Task DeleteAsync(string orderNumber)
        {
            var filter = Builders<Order>.Filter.Eq(x => x.OrderNumber, orderNumber);
            await _mongoDbContext.Orders.DeleteOneAsync(filter);
        }

        public async Task<Order> GetAsync(string orderNumber)
        {
            var filter = Builders<Order>.Filter.Eq(x => x.OrderNumber, orderNumber);
            var order = await _mongoDbContext.Orders.FindAsync(filter);
            return order.FirstOrDefault();
        }

        public async Task<ICollection<Order>> GetAllAsync(Guid merchantCode, string search, int skip, int limit, string orderBy)
        {

            var findOptions = new FindOptions<Order, Order>
            {
                Sort = orderBy.ToSortDefinition<Order>("States.CreatedDate"),
                Skip = skip,
                Limit = limit
            };

            ICollection<Order> orders;
            if (search is null)
            {
                orders = (await _mongoDbContext.Orders.FindAsync(new BsonDocument(), findOptions)).ToList();
            }
            else
            {
                var filter = Builders<Order>.Filter.Text(search);
                orders = (await _mongoDbContext.Orders.FindAsync(filter, findOptions)).ToList();
            }
            return orders;
        }

        public async Task UpdateAsync(Order order)
        {
            var filter = Builders<Order>.Filter.Eq(x => x.OrderNumber, order.OrderNumber);
            //var update = Builders<Order>.Update.set(order);
            //await _mongoDbContext.Orders.UpdateOneAsync(filter, order);
        }

        public async Task<bool> PushAsync(string orderNumber, State state)
        {
            var filter = Builders<Order>.Filter.Eq(x => x.OrderNumber, orderNumber);

            var push = Builders<Order>.Update.Push(x => x.States, state);

            var result = await _mongoDbContext.Orders.UpdateOneAsync(filter, push);
            if (result.IsAcknowledged)
                if (result.ModifiedCount > 0)
                    return true;
            return false;
        }
    }
}
