using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Marketplace.Common.Application.Commands;
using Marketplace.Common.Extensions;
using Marketplace.Data.Repositories.Interfaces;
using Marketplace.Domain.Merchant;
using Marketplace.Domain.Merchant.Rules;
using MediatR;

namespace Marketplace.Application.MerchantServices.MerchantUsers.Commands.RegisterMerchantUser
{
    public class RegisterMerchantCommandHandler : ICommandHandler<RegisterMerchantCommand, Unit>
    {
        private readonly IMerchantRepository _merchantRepository;
        private readonly IMerchantUserRepository _merchantUserRepository;
        private readonly IMerchantUserUniquenessChecker _merchantUserUniquenessChecker;

        public RegisterMerchantCommandHandler(IMerchantRepository merchantRepository, IMerchantUserRepository merchantUserRepository,
            IMerchantUserUniquenessChecker merchantUserUniquenessChecker)
        {
            _merchantRepository = merchantRepository;
            _merchantUserRepository = merchantUserRepository;
            _merchantUserUniquenessChecker = merchantUserUniquenessChecker;
        }

        public async Task<Unit> Handle(RegisterMerchantCommand request, CancellationToken cancellationToken)
        {
            var merchant = Merchant.Register(request.MerchantName);
            var merchantUser = MerchantUser.CreateRegisteredOnNewRegister(merchant.Code, merchant.Name, request.FirstName, request.LastName, request.Email,
                request.Phone, request.Password.AsMd5(), new List<string> { "account-admin" }, _merchantUserUniquenessChecker);

            await _merchantUserRepository.CreateAsync(merchantUser);
            await _merchantRepository.CreateAsync(merchant);

            return Unit.Task.Result;
        }
    }
}
