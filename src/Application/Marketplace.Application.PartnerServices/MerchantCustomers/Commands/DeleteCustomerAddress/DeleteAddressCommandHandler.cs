using Marketplace.Common.Application.Commands;
using Marketplace.Data.Repositories.Interfaces;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Marketplace.Application.MerchantServices.MerchantCustomers.Commands.DeleteCustomerAddress
{
    public class DeleteAddressCommandHandler : ICommandHandler<DeleteAddressCommand, bool>
    {
        private readonly IMerchantCustomerRepository _customerRepository;

        public DeleteAddressCommandHandler(IMerchantCustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<bool> Handle(DeleteAddressCommand request, CancellationToken cancellationToken)
        {
            var customer = await _customerRepository.GetAsync(request.Code);
            if(customer is null)
                throw new CustomerNotFoundException();

            var deletedAddress = customer.Addresses.FirstOrDefault(x => x.Code == request.AddressCode);

            var result = await _customerRepository.PullAddressAsync(request.Code, deletedAddress);

            return result;
        }
    }
}