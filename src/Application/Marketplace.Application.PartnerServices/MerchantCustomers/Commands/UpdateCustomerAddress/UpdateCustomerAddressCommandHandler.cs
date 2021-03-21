using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Marketplace.Common.Application.Commands;
using Marketplace.Data.Repositories.Interfaces;

namespace Marketplace.Application.MerchantServices.MerchantCustomers.Commands.UpdateCustomerAddress
{
    public class UpdateCustomerAddressCommandHandler : ICommandHandler<UpdateAddressCommand, CustomerDto>
    {
        private readonly IMerchantCustomerRepository _customerRepository;
        private readonly IMapper _mapper;

        public UpdateCustomerAddressCommandHandler(IMerchantCustomerRepository customerRepository, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        public async Task<CustomerDto> Handle(UpdateAddressCommand request, CancellationToken cancellationToken)
        {
            var customer = await _customerRepository.GetAsync(request.Code);
            if (customer is null)
                throw new CustomerNotFoundException();

            customer = customer.UpdateAddress(request.AddressCode, request.Name, request.City, request.Town, request.District, request.FullAddress, request.UpdateBy);
            var result = await _customerRepository.UpdateAsync(customer);
            var customerDto = _mapper.Map<CustomerDto>(result);
            return customerDto;
        }
    }
}
