using System.Threading;
using System.Threading.Tasks;
using Marketplace.Common.Application.Commands;
using Marketplace.Data.Repositories.Interfaces;
using MediatR;

namespace Marketplace.Application.MerchantServices.AvailableLocations.Commands.AddAvailableLocation
{
    public class AddAvailableLocationCommandHandler : ICommandHandler<AddAvailableLocationCommand>
    {
        private readonly IMerchantAddressRepository _merchantAddressRepository;

        public AddAvailableLocationCommandHandler(IMerchantAddressRepository merchantAddressRepository)
        {
            _merchantAddressRepository = merchantAddressRepository;

        }

        public async Task<Unit> Handle(AddAvailableLocationCommand request, CancellationToken cancellationToken)
        {
            var merchantLocation = await _merchantAddressRepository.GetAsync(request.MerchantCode, request.MerchantAddressCode);
            merchantLocation = merchantLocation.AddAvailableLocations(request.Locations);
            await _merchantAddressRepository.UpdateAsync(merchantLocation);

            return Unit.Task.Result;
        }
    }
}
