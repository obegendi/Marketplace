using System.Threading;
using System.Threading.Tasks;
using Marketplace.Common.Application.Commands;
using Marketplace.Data.Repositories.Interfaces;

namespace Marketplace.Application.MerchantServices.MerchantCustomers.Commands.DeleteCustomer
{
    public class DeleteCustomerCommandHandler : ICommandHandler<DeleteCustomerCommand, bool>
    {
        private readonly IMerchantCustomerRepository _customerRepository;

        public DeleteCustomerCommandHandler(IMerchantCustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<bool> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
        {
            var result = await _customerRepository.DeleteAsync(request.Code);
            return result;
        }
    }
}