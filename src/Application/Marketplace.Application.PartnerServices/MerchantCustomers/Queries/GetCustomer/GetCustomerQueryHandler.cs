using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Marketplace.Common.Application.Queries;
using Marketplace.Data.Repositories.Interfaces;

namespace Marketplace.Application.MerchantServices.MerchantCustomers.Queries.GetCustomer
{
    public class GetCustomerQueryHandler : IQueryHandler<GetCustomerQuery, CustomerDto>
    {
        private readonly IMerchantCustomerRepository _customerRepository;
        private readonly IMapper _mapper;

        public GetCustomerQueryHandler(IMerchantCustomerRepository customerRepository, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        public async Task<CustomerDto> Handle(GetCustomerQuery request, CancellationToken cancellationToken)
        {
            var customer = await _customerRepository.GetAsync(request.Code);
            if (customer is null)
                throw new CustomerNotFoundException();

            var customerDto = _mapper.Map<CustomerDto>(customer);
            return customerDto;
        }
    }
}
