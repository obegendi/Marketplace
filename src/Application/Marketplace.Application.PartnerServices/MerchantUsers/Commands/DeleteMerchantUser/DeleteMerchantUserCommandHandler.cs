using Marketplace.Common.Application.Commands;
using Marketplace.Data.Repositories.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace Marketplace.Application.MerchantServices.MerchantUsers.Commands.DeleteMerchantUser
{
    public class DeleteMerchantUserCommandHandler : ICommandHandler<DeleteMerchantUserCommand, bool>
    {
        private readonly IMerchantUserRepository _merchantUserRepository;

        public DeleteMerchantUserCommandHandler(IMerchantUserRepository merchantUserRepository)
        {
            _merchantUserRepository = merchantUserRepository;
        }

        public async Task<bool> Handle(DeleteMerchantUserCommand request, CancellationToken cancellationToken)
        {
            var response = await _merchantUserRepository.DeleteAsync(request.MerchantCode, request.Code);

            return response;

        }
    }
}
