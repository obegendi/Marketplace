using System.Threading;
using System.Threading.Tasks;
using MediatR.Pipeline;

namespace Marketplace.API.Infrastructure
{
    public class GenericRequestPostProcessor<TRequest, TResponse> : IRequestPostProcessor<TRequest, TResponse>
    {
        public Task Process(TRequest request, TResponse response, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }

}
