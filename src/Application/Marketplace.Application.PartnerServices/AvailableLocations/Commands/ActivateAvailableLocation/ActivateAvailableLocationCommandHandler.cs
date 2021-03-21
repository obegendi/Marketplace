using System.Threading;
using System.Threading.Tasks;
using Marketplace.Common.Application.Commands;
using Marketplace.Data.Repositories.Interfaces;
using MediatR;

namespace Marketplace.Application.MerchantServices.AvailableLocations.Commands.ActivateAvailableLocation
{
    public class ActivateAvailableLocationCommandHandler : ICommandHandler<ActivateAvailableLocationCommand>
    {
        private readonly IMerchantAddressRepository _merchantAddressRepository;

        public ActivateAvailableLocationCommandHandler(IMerchantAddressRepository merchantAddressRepository)
        {
            _merchantAddressRepository = merchantAddressRepository;
        }

        public async Task<Unit> Handle(ActivateAvailableLocationCommand request, CancellationToken cancellationToken)
        {
            var merchantLocation = await _merchantAddressRepository.GetAsync(request.MerchantCode, request.MerchantAddressCode);

            merchantLocation = merchantLocation.ActiveAvailableLocations(request.Locations);

            await _merchantAddressRepository.UpdateAsync(merchantLocation);

            return Unit.Task.Result;
        }
    }
}
