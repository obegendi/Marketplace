using System.Threading.Tasks;
using MediatR;

namespace Marketplace.API.Infrastructure
{
    public class DomainEventsDispatcher : IDomainsEventsDispatcher
    {
        private readonly IMediator _mediator;

        public DomainEventsDispatcher(IMediator mediator)
        {

            _mediator = mediator;
        }

        public async Task DispatchEventsAsync()
        {

        }
    }

}
