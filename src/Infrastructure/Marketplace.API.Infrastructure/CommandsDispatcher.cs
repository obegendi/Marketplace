using System;
using System.Threading.Tasks;
using MediatR;

namespace Marketplace.API.Infrastructure
{
    public class CommandsDispatcher : ICommandsDispatcher
    {
        private readonly IMediator _mediator;

        public CommandsDispatcher(IMediator mediator)
        {
            _mediator = mediator;
        }

        public Task DispatchCommandAsync(Guid guid)
        {
            //var internalCommand = 

            return Task.CompletedTask;
        }
    }
}
