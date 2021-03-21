using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Marketplace.Common.Application.Commands;
using Marketplace.Data.Repositories.Interfaces;

namespace Marketplace.Application.MerchantServices.MerchantCustomers.Commands.UpdateCustomer
{
    public class UpdateCustomerCommandHandler : ICommandHandler<UpdateCustomerCommand, CustomerDto>
    {
        private readonly IMerchantCustomerRepository _merchantCustomerRepository;
        private readonly IMapper _mapper;

        public UpdateCustomerCommandHandler(IMerchantCustomerRepository merchantCustomerRepository, IMapper mapper)
        {
            _merchantCustomerRepository = merchantCustomerRepository;
            _mapper = mapper;
        }

        public async Task<CustomerDto> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = await _merchantCustomerRepository.GetAsync(request.CustomerCode);
            if (customer is null)
                throw new CustomerNotFoundException();
            customer = customer.Update(request.Email, request.Phone, request.FirstName, request.LastName, request.UpdatedBy, request.Addresses);
            var result = await _merchantCustomerRepository.UpdateAsync(customer);
            var customerDto = _mapper.Map<CustomerDto>(result);
            return customerDto;
        }
    }
}