using System.Threading;
using System.Threading.Tasks;
using Marketplace.API.Infrastructure;
using Marketplace.Common.Application.Queries;
using Marketplace.Data.Repositories.Interfaces;
using Marketplace.Domain.SharedKernel;

namespace Marketplace.Application.MerchantServices.AvailableLocations.Queries.GetAvailableLocations
{
    public class GetAvailableLocationsQueryHandler : IQueryHandler<GetAvailableLocationsQuery, BaseListResponseModel<Location>>
    {
        private readonly IMerchantAddressRepository _merchantAddressRepository;

        public GetAvailableLocationsQueryHandler(IMerchantAddressRepository merchantAddressRepository)
        {
            _merchantAddressRepository = merchantAddressRepository;
        }

        public async Task<BaseListResponseModel<Location>> Handle(GetAvailableLocationsQuery request, CancellationToken cancellationToken)
        {
            var merchantAddress = await _merchantAddressRepository.GetAsync(request.MerchantCode, request.MerchantAddressCode);
            return new BaseListResponseModel<Location>(request.Limit, merchantAddress.AvailableLocations);
        }
    }
}
