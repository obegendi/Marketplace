using System.Threading;
using System.Threading.Tasks;
using Marketplace.Common.Application.Commands;
using Marketplace.Data.Repositories.Interfaces;
using MediatR;

namespace Marketplace.Application.MerchantServices.AvailableLocations.Commands.RemoveAvailableLocation
{
    public class RemoveAvailableLocationCommandHandler : ICommandHandler<RemoveAvailableLocationCommand>
    {
        private readonly IMerchantAddressRepository _merchantAddressRepository;
        public RemoveAvailableLocationCommandHandler(IMerchantAddressRepository merchantAddressRepository)
        {
            _merchantAddressRepository = merchantAddressRepository;
        }

        /// <inheritdoc />
        public async Task<Unit> Handle(RemoveAvailableLocationCommand request, CancellationToken cancellationToken)
        {
            await _merchantAddressRepository.PullAvailableLocationsAsync(request.MerchantCode, request.MerchantAddressCode, request.Locations);

            return Unit.Task.Result;
        }
    }
}
