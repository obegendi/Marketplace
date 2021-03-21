using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Marketplace.API.Infrastructure;
using Marketplace.Common.Application.Queries;
using Marketplace.Data.Repositories.Interfaces;
using Marketplace.Domain.Order;

namespace Marketplace.Application.OrderServices.GetMerchantOrders
{
    public class MerchantOrdersQueryHandler : IQueryHandler<GetMerchantOrdersQuery, BaseListResponseModel<OrderDto>>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public MerchantOrdersQueryHandler(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        public async Task<BaseListResponseModel<OrderDto>> Handle(GetMerchantOrdersQuery request, CancellationToken cancellationToken)
        {
            var orders = await _orderRepository.GetAllAsync(request.MerchantCode, request.Search, request.Skip, request.Limit, request.OrderBy);
            if (orders == null || !orders.Any())
                throw new MerchantOrderNotFoundException();

            var orderDto = _mapper.Map<List<OrderDto>>(orders);
           
            return new BaseListResponseModel<OrderDto>(request.Limit, orderDto); ;
        }
    }


}
