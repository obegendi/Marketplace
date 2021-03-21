using System.Threading;
using System.Threading.Tasks;
using Marketplace.Common.Application.Commands;
using Marketplace.Data.Repositories.Interfaces;
using MediatR;

namespace Marketplace.Application.MerchantServices.MerchantAddresses.Commands.DeactivateMerchantAddress
{
    public class DeactivateMerchantAddressCommandHandler : ICommandHandler<DeactivateMerchantAddressCommand>
    {
        private readonly IMerchantAddressRepository _merchantAddressRepository;
        private readonly IMerchantRepository _merchantRepository;

        public DeactivateMerchantAddressCommandHandler(IMerchantAddressRepository merchantAddressRepository, IMerchantRepository merchantRepository)
        {
            _merchantAddressRepository = merchantAddressRepository;
            _merchantRepository = merchantRepository;
        }

        public async Task<Unit> Handle(DeactivateMerchantAddressCommand request, CancellationToken cancellationToken)
        {
            var merchantLocation = await _merchantAddressRepository.GetAsync(request.MerchantCode, request.MerchantAddressCode);
            if (merchantLocation is null)
                throw new MerchantAddressNotFoundException();

            merchantLocation.Deactive();
            await _merchantAddressRepository.UpdateIsActiveAsync(merchantLocation.MerchantName, merchantLocation.Name, merchantLocation.IsActive);

            await _merchantRepository.RemoveAddressMetaDataAsync(merchantLocation.MerchantName, merchantLocation.Name);

            return Unit.Task.Result;
        }
    }
}
