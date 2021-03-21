using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Marketplace.API.Infrastructure;
using Marketplace.Common.Application.Queries;
using Marketplace.Data.Repositories.Interfaces;

namespace Marketplace.Application.CustomerServices.Queries.GetCustomers
{
    public class GetCustomersQueryHandler : IQueryHandler<GetCustomersQuery, BaseListResponseModel<CustomerDto>>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;

        public GetCustomersQueryHandler(ICustomerRepository customerRepository, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        public async Task<BaseListResponseModel<CustomerDto>> Handle(GetCustomersQuery request, CancellationToken cancellationToken)
        {
            var customerList = await _customerRepository.GetAllAsync(request.MerchantCode, request.Search, request.Skip, request.Limit, request.OrderBy);

            var customerDtoList = _mapper.Map<List<CustomerDto>>(customerList);

            return new BaseListResponseModel<CustomerDto>(request.Limit, customerDtoList);
        }
    }
}