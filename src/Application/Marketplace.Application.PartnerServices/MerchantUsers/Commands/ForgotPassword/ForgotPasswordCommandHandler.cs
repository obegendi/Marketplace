using System.Threading;
using System.Threading.Tasks;
using Marketplace.Common.Application.Commands;
using Marketplace.Data.Repositories.Interfaces;
using MediatR;

namespace Marketplace.Application.MerchantServices.MerchantUsers.Commands.ForgotPassword
{
    public class ForgotPasswordCommandHandler : ICommandHandler<ForgotPasswordCommand>
    {
        private readonly IMerchantUserRepository _accountUserRepository;

        public ForgotPasswordCommandHandler(IMerchantUserRepository accountUserRepository)
        {
            _accountUserRepository = accountUserRepository;
        }

        public async Task<Unit> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
        {
            var account = await _accountUserRepository.GetWithPhoneAsync(request.Phone).ConfigureAwait(true);
            if (account is null)
                throw new MerchantUserNotNotFound();

            await _accountUserRepository.UpdatePasswordAsync(account);

            return Unit.Task.Result;
        }
    }
}
