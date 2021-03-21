using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Marketplace.Common.Application.Commands;
using Marketplace.Data.Repositories.Interfaces;
using Marketplace.Domain.Merchant.Customer;

namespace Marketplace.Application.CustomerServices.Commands.RegisterCustomer
{
    public class RegisterCustomerCommandHandler : ICommandHandler<RegisterCustomerCommand, CustomerDto>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;

        public RegisterCustomerCommandHandler(ICustomerRepository customerRepository, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        public async Task<CustomerDto> Handle(RegisterCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = MerchantCustomer.CreateNew(request.Email, request.Phone, request.Name, request.Surname,
                request.CreateBy);
            if (customer is null)
                throw new CustomerNotFoundException();

            await _customerRepository.InsertAsync(customer);
            var customerDto = _mapper.Map<CustomerDto>(customer);
            return customerDto;
        }
    }
}
