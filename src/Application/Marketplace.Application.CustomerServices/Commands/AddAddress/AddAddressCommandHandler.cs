using Marketplace.Common.Application.Commands;
using Marketplace.Data.Repositories.Interfaces;
using Marketplace.Domain.SharedKernel;
using System.Threading;
using System.Threading.Tasks;

namespace Marketplace.Application.CustomerServices.Commands.AddAddress
{
    public class AddAddressCommandHandler : ICommandHandler<AddAddressCommand, bool>
    {
        private readonly ICustomerRepository _customerRepository;

        public AddAddressCommandHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<bool> Handle(AddAddressCommand request, CancellationToken cancellationToken)
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

            return result;
        }
    }
}
