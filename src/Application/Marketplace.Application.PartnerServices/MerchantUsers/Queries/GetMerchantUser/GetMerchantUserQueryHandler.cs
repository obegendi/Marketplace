using System.Threading;
using System.Threading.Tasks;
using Marketplace.Common.Application.Queries;
using Marketplace.Data.Repositories.Interfaces;

namespace Marketplace.Application.MerchantServices.MerchantUsers.Queries.GetMerchantUser
{
    public class GetMerchantUserQueryHandler : IQueryHandler<GetMerchantUserQuery, MerchantUserDto>
    {
        private readonly IMerchantUserRepository _merchantUserRepository;

        public GetMerchantUserQueryHandler(IMerchantUserRepository merchantUserRepository)
        {
            _merchantUserRepository = merchantUserRepository;
        }

        public async Task<MerchantUserDto> Handle(GetMerchantUserQuery request, CancellationToken cancellationToken)
        {
            var user = await _merchantUserRepository.GetAsync(request.Phone);
            var userDto = new MerchantUserDto
            {
                Claims = user.Claims,
                Phone = user.Phone,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                IsActive = user.IsActive
            };
            return userDto;
        }
    }
}
