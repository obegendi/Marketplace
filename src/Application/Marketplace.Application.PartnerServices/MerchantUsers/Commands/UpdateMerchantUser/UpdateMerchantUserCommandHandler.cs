using System.Threading;
using System.Threading.Tasks;
using Marketplace.Common.Application.Commands;
using Marketplace.Data.Repositories.Interfaces;
using MediatR;
using MongoDB.Driver;

namespace Marketplace.Application.MerchantServices.MerchantUsers.Commands.UpdateMerchantUser
{
    public class UpdateMerchantUserCommandHandler : ICommandHandler<UpdateMerchantUserCommand, bool>
    {
        private readonly IMerchantUserRepository _accountUserRepository;

        public UpdateMerchantUserCommandHandler(IMerchantUserRepository accountUserRepository)
        {
            _accountUserRepository = accountUserRepository;
        }

        public async Task<bool> Handle(UpdateMerchantUserCommand request, CancellationToken cancellationToken)
        {
            var result = await _accountUserRepository.UpdateAsync(request.UserPhone, request.Phone, request.Email, request.FirstName, request.LastName, request.Password,
                request.IsActive, request.Claims);

            return result;
        }
    }
}
