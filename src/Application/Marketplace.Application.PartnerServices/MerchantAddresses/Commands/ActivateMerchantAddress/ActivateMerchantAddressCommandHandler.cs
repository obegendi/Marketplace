using System.Threading;
using System.Threading.Tasks;
using Marketplace.Common.Application.Commands;
using Marketplace.Data.Repositories.Interfaces;
using MediatR;

namespace Marketplace.Application.MerchantServices.MerchantAddresses.Commands.ActivateMerchantAddress
{
    public class ActivateMerchantAddressCommandHandler : ICommandHandler<ActivateMerchantAddressCommand>
    {
        private readonly IMerchantAddressRepository _merchantAddressRepository;
        private readonly IMerchantRepository _merchantRepository;

        public ActivateMerchantAddressCommandHandler(IMerchantAddressRepository merchantAddressRepository, IMerchantRepository merchantRepository)
        {
            _merchantAddressRepository = merchantAddressRepository;
            _merchantRepository = merchantRepository;
        }

        public async Task<Unit> Handle(ActivateMerchantAddressCommand request, CancellationToken cancellationToken)
        {
            var merchantAddress = await _merchantAddressRepository.GetAsync(request.MerchantCode, request.MerchantAddressCode);
            if (merchantAddress is null)
                throw new MerchantAddressNotFoundException();

            merchantAddress.Activate();
            await _merchantAddressRepository.UpdateIsActiveAsync(merchantAddress.MerchantName, merchantAddress.Name, merchantAddress.IsActive);

            await _merchantRepository.AddAddressMetaDataAsync(merchantAddress.MerchantName, merchantAddress.Name);

            return Unit.Task.Result;
        }
    }
}
