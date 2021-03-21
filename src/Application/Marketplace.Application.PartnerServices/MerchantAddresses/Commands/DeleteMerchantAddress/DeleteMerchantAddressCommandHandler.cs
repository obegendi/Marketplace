using System.Threading;
using System.Threading.Tasks;
using Marketplace.Common.Application.Commands;
using Marketplace.Data.Repositories.Interfaces;
using MediatR;

namespace Marketplace.Application.MerchantServices.MerchantAddresses.Commands.DeleteMerchantAddress
{
    public class DeleteMerchantAddressCommandHandler : ICommandHandler<DeleteMerchantAddressCommand>
    {
        private readonly IMerchantAddressRepository _merchantAddressRepository;

        public DeleteMerchantAddressCommandHandler(IMerchantAddressRepository merchantAddressRepository)
        {
            _merchantAddressRepository = merchantAddressRepository;
        }

        public async Task<Unit> Handle(DeleteMerchantAddressCommand request, CancellationToken cancellationToken)
        {
            await _merchantAddressRepository.DeleteAsync(request.MerchantCode, request.MerchantAddressCode);

            return Unit.Task.Result;
        }
    }
}
