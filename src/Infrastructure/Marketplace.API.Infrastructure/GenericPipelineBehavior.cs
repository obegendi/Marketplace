using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace Marketplace.API.Infrastructure
{
    public class GenericPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        public Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            return next.Invoke();
        }
    }
}
