using System.Threading;
using System.Threading.Tasks;
using Marketplace.Common.Application.Queries;
using Marketplace.Data.Repositories.Interfaces;

namespace Marketplace.Application.MerchantServices.MerchantAddresses.Queries.GetMerchantAddress
{
    public class GetMerchantAddressQueryHandler : IQueryHandler<GetMerchantAddressQuery, MerchantAddressDto>
    {
        private readonly IMerchantAddressRepository _merchantAddressRepository;

        public GetMerchantAddressQueryHandler(IMerchantAddressRepository merchantAddressRepository)
        {
            _merchantAddressRepository = merchantAddressRepository;
        }
        public async Task<MerchantAddressDto> Handle(GetMerchantAddressQuery request, CancellationToken cancellationToken)
        {
            var address = await _merchantAddressRepository.GetAsync(request.MerchantCode, request.MerchantAddressCode);
            if (address is null)
                throw new MerchantAddressNotFoundException("Merchant Address Not Found");
            var addressDto = new MerchantAddressDto
            {
                Code = address.Code,
                MerchantName = address.MerchantName,
                Name = address.Name,
                Address = address.Address,
                Country = address.Country,
                State = address.State,
                City = address.City,
                Town = address.Town,
                District = address.District,
                Location = address.Location,
                ExtraInfo = address.ExtraInfo,
                WorkingHours = address.WorkingHours,
                IsActive = address.IsActive
            };

            return addressDto;
        }
    }
}
