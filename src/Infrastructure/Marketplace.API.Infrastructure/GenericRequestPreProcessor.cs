using System.Threading;
using System.Threading.Tasks;
using MediatR.Pipeline;

namespace Marketplace.API.Infrastructure
{
    public class GenericRequestPreProcessor<TRequest> : IRequestPreProcessor<TRequest>
    {

        public Task Process(TRequest request, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
