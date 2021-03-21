using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Marketplace.Common.Application.Commands;
using Marketplace.Data.Repositories.Interfaces;
using Marketplace.Domain.SharedKernel;

namespace Marketplace.Application.MerchantServices.MerchantCustomers.Commands.AddAddress
{
    public class AddAddressCommandHandler : ICommandHandler<AddAddressCommand, CustomerDto>
    {
        private readonly IMerchantCustomerRepository _customerRepository;
        private readonly IMapper _mapper;

        public AddAddressCommandHandler(IMerchantCustomerRepository customerRepository, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        public async Task<CustomerDto> Handle(AddAddressCommand request, CancellationToken cancellationToken)
        {
            var customer = await _customerRepository.GetAsync(request.Code);
            if (customer is null)
                throw new CustomerNotFoundException();

            customer.AddAddress(new Address
            {
                City = request.City,
                District = request.District,
                Name = request.Name,
                Town = request.Town,
                FullAddress = request.FullAddress
            });

            var result = await _customerRepository.UpdateAsync(customer);
            var customerDto = _mapper.Map<CustomerDto>(result);
            return customerDto;
        }
    }
}
