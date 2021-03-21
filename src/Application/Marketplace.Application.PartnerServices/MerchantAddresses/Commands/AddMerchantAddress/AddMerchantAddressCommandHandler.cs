using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Marketplace.Application.MerchantServices.MerchantAddresses.Queries.GetMerchantAddress;
using Marketplace.Common.Application.Commands;
using Marketplace.Data.Repositories.Interfaces;
using Marketplace.Domain.Merchant;

namespace Marketplace.Application.MerchantServices.MerchantAddresses.Commands.AddMerchantAddress
{
    public class AddMerchantAddressCommandHandler : ICommandHandler<AddMerchantAddressCommand, MerchantAddressDto>
    {
        private readonly IMapper _mapper;
        private readonly IMerchantAddressRepository _merchantLocationsRepository;
        private readonly IMerchantRepository _merchantRepository;

        public AddMerchantAddressCommandHandler(IMerchantAddressRepository merchantLocationsRepository, IMerchantRepository merchantRepository,
            IMapper mapper)
        {
            _merchantLocationsRepository = merchantLocationsRepository;
            _merchantRepository = merchantRepository;
            _mapper = mapper;
        }

        public async Task<MerchantAddressDto> Handle(AddMerchantAddressCommand request, CancellationToken cancellationToken)
        {
            var merchant = await _merchantRepository.GetAsync(request.MerchantCode);
            if (merchant is null)
                throw new MerchantNotFoundException();

            var merchantLocation = MerchantAddress.RegisterCreated(
                request.MerchantCode,
                request.MerchantName,
                request.Name,
                request.Address,
                request.Country,
                request.City,
                request.Town,
                request.District,
                request.Location,
                request.WorkingHours,
                request.ExtraInfo);

            merchantLocation.Activate();
            var locations = await _merchantLocationsRepository.GetAsync(request.MerchantCode, request.Name);
            if (locations != null)
                throw new MerchantAddressAlreadyRegisteredException();

            await _merchantLocationsRepository.AddAsync(merchantLocation);
            return _mapper.Map<MerchantAddressDto>(merchantLocation);
        }
    }
}
