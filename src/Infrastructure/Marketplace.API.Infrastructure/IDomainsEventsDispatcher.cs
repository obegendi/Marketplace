using System.Threading.Tasks;

namespace Marketplace.API.Infrastructure
{
    public interface IDomainsEventsDispatcher
    {
        Task DispatchEventsAsync();
    }
}
