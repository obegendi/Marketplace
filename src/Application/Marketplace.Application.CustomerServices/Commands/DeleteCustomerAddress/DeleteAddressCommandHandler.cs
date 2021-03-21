using System.Threading;
using System.Threading.Tasks;
using Marketplace.Common.Application.Commands;
using Marketplace.Data.Repositories.Interfaces;

namespace Marketplace.Application.CustomerServices.Commands.DeleteCustomerAddress
{
    public class DeleteAddressCommandHandler : ICommandHandler<DeleteAddressCommand, bool>
    {
        private readonly ICustomerRepository _customerRepository;

        public DeleteAddressCommandHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<bool> Handle(DeleteAddressCommand request, CancellationToken cancellationToken)
        {
            var result = await _customerRepository.DeleteAddressAsync(request.Code, request.AddressCode);

            return result;
        }
    }
}