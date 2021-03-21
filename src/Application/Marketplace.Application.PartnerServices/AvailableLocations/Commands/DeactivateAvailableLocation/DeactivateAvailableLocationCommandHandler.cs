using System.Threading;
using System.Threading.Tasks;
using Marketplace.Common.Application.Commands;
using Marketplace.Data.Repositories.Interfaces;
using MediatR;

namespace Marketplace.Application.MerchantServices.AvailableLocations.Commands.DeactivateAvailableLocation
{
    public class DeactivateAvailableLocationCommandHandler : ICommandHandler<DeactivateAvailableLocationCommand>
    {
        private readonly IMerchantAddressRepository _merchantAddressRepository;

        public DeactivateAvailableLocationCommandHandler(IMerchantAddressRepository merchantAddressRepository)
        {
            _merchantAddressRepository = merchantAddressRepository;
        }

        public async Task<Unit> Handle(DeactivateAvailableLocationCommand request, CancellationToken cancellationToken)
        {
            var merchantLocation = await _merchantAddressRepository.GetAsync(request.MerchantCode, request.MerchantAddressCode);
            merchantLocation = merchantLocation.DeactiveAvailableLocations(request.Locations);
            await _merchantAddressRepository.UpdateAsync(merchantLocation);

            return Unit.Task.Result;
        }
    }
}
