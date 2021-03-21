using MediatR;

namespace Marketplace.Common.Application.Queries
{
    public interface IQuery<out TResult> : IRequest<TResult>
    {
    }
}
