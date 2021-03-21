using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Marketplace.Common;
using Marketplace.Common.Application.Commands;
using Marketplace.Data.Repositories.Interfaces;
using MediatR;

namespace Marketplace.Application.OrderServices.CollectOrder
{
    public class CollectOrderCommandHandler : ICommandHandler<CollectOrderCommand>
    {
        private readonly IOrderRepository _orderRepository;

        public CollectOrderCommandHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<Unit> Handle(CollectOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetAsync(request.OrderNumber);
            if (order is null)
                throw new OrderNotFoundException();

            order = order.StartCollect();
            var collectState = order.States.FirstOrDefault(x => x.StateName == Consts.Order.Collecting);
            await _orderRepository.PushAsync(order.OrderNumber, collectState);

            return Unit.Task.Result;
        }
    }
}
