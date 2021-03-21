using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Marketplace.Common.Application.Queries;
using Marketplace.Data.Repositories.Interfaces;

namespace Marketplace.Application.OrderServices.GetOrder
{
    public class GetOrderQueryHandler : IQueryHandler<GetOrderQuery, OrderDto>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public GetOrderQueryHandler(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        public async Task<OrderDto> Handle(GetOrderQuery request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetAsync(request.OrderNumber);
            if (order is null)
                throw new OrderNotFoundException();
            var orderDto = _mapper.Map<OrderDto>(order);

            return orderDto;
        }
    }
}
