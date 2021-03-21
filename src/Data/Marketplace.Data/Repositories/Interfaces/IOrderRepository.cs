using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Marketplace.Domain.Order;

namespace Marketplace.Data.Repositories.Interfaces
{
    public interface IOrderRepository
    {
        Task<Order> GetAsync(string orderNumber);
        Task<ICollection<Order>> GetAllAsync(Guid merchantCode, string search, int skip, int limit, string orderBy);
        Task UpdateAsync(Order order);
        Task CreateAsync(Order order);
        Task DeleteAsync(string orderNumber);
        Task<bool> PushAsync(string orderNumber, State state);
    }
}
