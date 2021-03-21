using System.Threading;
using System.Threading.Tasks;
using Marketplace.Common.Application.Commands;
using Marketplace.Data.Repositories.Interfaces;

namespace Marketplace.Application.CustomerServices.Commands.UpdateCustomerAddress
{
    public class UpdateCustomerAddressCommandHandler : ICommandHandler<UpdateAddressCommand, bool>
    {
        private readonly ICustomerRepository _customerRepository;

        public UpdateCustomerAddressCommandHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<bool> Handle(UpdateAddressCommand request, CancellationToken cancellationToken)
        {
            var customer = await _customerRepository.GetAsync(request.Code);
            if (customer is null)
                throw new CustomerNotFoundException();

            customer = customer.UpdateAddress(request.Code, request.Name, request.City, request.Town, request.District, request.FullAddress, request.UpdateBy);
            var result = await _customerRepository.UpdateAsync(customer);

            return result;
        }
    }
}
