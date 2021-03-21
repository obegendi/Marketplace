using AutoMapper;
using Marketplace.Application.MerchantServices.MerchantAddresses.Queries.GetMerchantAddress;
using Marketplace.Common.Application.Commands;
using Marketplace.Data.Repositories.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace Marketplace.Application.MerchantServices.MerchantAddresses.Commands.UpdateMerchantAddress
{
    public class UpdateMerchantAddressCommandHandler : ICommandHandler<UpdateMerchantAddressCommand, MerchantAddressDto>
    {
        private readonly IMapper _mapper;
        private readonly IMerchantAddressRepository _merchantAddressRepository;
        private readonly IMerchantRepository _merchantRepository;

        public UpdateMerchantAddressCommandHandler(IMerchantRepository merchantRepository, IMerchantAddressRepository merchantAddressRepository,
            IMapper mapper)
        {
            _merchantRepository = merchantRepository;
            _merchantAddressRepository = merchantAddressRepository;
            _mapper = mapper;
        }
        public async Task<MerchantAddressDto> Handle(UpdateMerchantAddressCommand request, CancellationToken cancellationToken)
        {
            var merchant = await _merchantRepository.GetAsync(request.MerchantCode);
            if (merchant is null)
                throw new MerchantNotFoundException();

            var merchantLocation = await _merchantAddressRepository.GetAsync(request.MerchantCode, request.MerchantAddressCode);
            if (merchantLocation is null)
                throw new ActiveMerchantLocationNotFoundException();

            merchantLocation.Update(request.Name, request.Address, request.City, request.Town, request.District,
                request.Location, request.WorkingHours, request.ExtraInfo);
            await _merchantAddressRepository.UpdateAsync(merchantLocation);
            return _mapper.Map<MerchantAddressDto>(merchantLocation);
        }
    }
}
