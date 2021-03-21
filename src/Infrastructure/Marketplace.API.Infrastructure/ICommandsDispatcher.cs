using System;
using System.Threading.Tasks;

namespace Marketplace.API.Infrastructure
{
    public interface ICommandsDispatcher
    {
        Task DispatchCommandAsync(Guid guid);
    }
}
