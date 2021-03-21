using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Marketplace.Common;
using Marketplace.Common.Application.Commands;
using Marketplace.Data.Repositories.Interfaces;
using MediatR;

namespace Marketplace.Application.OrderServices.DeliverOrder
{
    public class DeliverOrderCommandHandler : ICommandHandler<DeliverOrderCommand>
    {
        private readonly IOrderRepository _orderRepository;

        public DeliverOrderCommandHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<Unit> Handle(DeliverOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetAsync(request.OrderNumber);
            if (order is null)
                throw new OrderNotFoundException();

            order = order.Deliver();
            var collectState = order.States.FirstOrDefault(x => x.StateName == Consts.Order.Delivered);
            await _orderRepository.PushAsync(order.OrderNumber, collectState);

            return Unit.Task.Result;
        }
    }
}
