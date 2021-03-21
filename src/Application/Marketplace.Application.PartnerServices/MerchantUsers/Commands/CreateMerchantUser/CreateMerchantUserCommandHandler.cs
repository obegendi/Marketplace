using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Marketplace.Common.Application.Commands;
using Marketplace.Common.Extensions;
using Marketplace.Data.Repositories.Interfaces;
using Marketplace.Domain.Merchant;
using Marketplace.Domain.Merchant.Rules;

namespace Marketplace.Application.MerchantServices.MerchantUsers.Commands.CreateMerchantUser
{
    public class CreateMerchantUserCommandHandler : ICommandHandler<CreateMerchantUserCommand, MerchantUserDto>
    {
        private readonly IMapper _mapper;
        private readonly IMerchantUserRepository _merchantUserRepository;
        private readonly IMerchantUserUniquenessChecker _merchantUserUniquenessChecker;

        public CreateMerchantUserCommandHandler(IMerchantUserRepository merchantUserRepository, IMerchantUserUniquenessChecker merchantUserUniquenessChecker,
            IMapper mapper)
        {
            _merchantUserRepository = merchantUserRepository;
            _merchantUserUniquenessChecker = merchantUserUniquenessChecker;
            _mapper = mapper;
        }

        public async Task<MerchantUserDto> Handle(CreateMerchantUserCommand request, CancellationToken cancellationToken)
        {
            var accountUser = MerchantUser.CreateRegisteredByMerchant(request.MerchantCode, request.MerchantName, request.FirstName, request.LastName,
                request.Password.AsMd5(),
                request.Email, request.Phone, request.IsActive, request.Claims, _merchantUserUniquenessChecker);

            await _merchantUserRepository.CreateAsync(accountUser);

            return _mapper.Map<MerchantUserDto>(accountUser);
        }
    }
}
