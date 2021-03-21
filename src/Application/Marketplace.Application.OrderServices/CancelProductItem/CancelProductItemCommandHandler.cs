using System.Threading;
using System.Threading.Tasks;
using Marketplace.Common.Application.Commands;
using Marketplace.Data.Repositories.Interfaces;
using MediatR;

namespace Marketplace.Application.OrderServices.CancelProductItem
{
    public class CancelProductItemCommandHandler : ICommandHandler<CancelProductItemCommand>
    {
        private readonly IOrderRepository _orderRepository;

        public CancelProductItemCommandHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<Unit> Handle(CancelProductItemCommand request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetAsync(request.OrderNumber);
            if (order is null)
                throw new OrderNotFoundException();

            order.CancelLineItem(request.Sku);

            return Unit.Task.Result;
        }
    }
}
