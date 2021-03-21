using AutoMapper;
using Marketplace.API.Infrastructure;
using Marketplace.Application.MerchantServices.MerchantAddresses.Queries.GetMerchantAddress;
using Marketplace.Common.Application.Queries;
using Marketplace.Data.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Marketplace.Application.MerchantServices.MerchantAddresses.Queries.GetMerchantAddressList
{
    public class GetMerchantAddressListQueryHandler : IQueryHandler<GetMerchantAddressListQuery, BaseListResponseModel<MerchantAddressDto>>
    {
        private readonly IMerchantAddressRepository _merchantAddressRepository;
        private readonly IMapper _mapper;

        public GetMerchantAddressListQueryHandler(IMerchantAddressRepository merchantAddressRepository, IMapper mapper)
        {
            _merchantAddressRepository = merchantAddressRepository;
            _mapper = mapper;
        }

        public async Task<BaseListResponseModel<MerchantAddressDto>> Handle(GetMerchantAddressListQuery request, CancellationToken cancellationToken)
        {
            var address = await _merchantAddressRepository.GetAllAsync(request.MerchantCode, request.Search, request.Skip, request.Limit, request.OrderBy);
            var addressDto = _mapper.Map<List<MerchantAddressDto>>(address);

            return new BaseListResponseModel<MerchantAddressDto>(request.Limit, addressDto);

        }
    }
}
