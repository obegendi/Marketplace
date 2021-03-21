using Marketplace.API.Infrastructure;
using Marketplace.Common.Application.Queries;
using Marketplace.Data.Repositories.Interfaces;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Marketplace.Application.MerchantServices.MerchantUsers.Queries.GetMerchantUsers
{
    public class GetMerchantUsersQueryHandler : IQueryHandler<GetMerchantUsersQuery, BaseListResponseModel<MerchantUserDto>>
    {
        private readonly IMerchantUserRepository _merchantUserRepository;

        public GetMerchantUsersQueryHandler(IMerchantUserRepository merchantUserRepository)
        {
            _merchantUserRepository = merchantUserRepository;
        }

        public async Task<BaseListResponseModel<MerchantUserDto>> Handle(GetMerchantUsersQuery request, CancellationToken cancellationToken)
        {
            var entity = await _merchantUserRepository.GetAllAsync(request.MerchantCode, request.Search, request.Limit, request.Skip, request.OrderBy);

            if (entity is null)
                throw new MerchantUserNotFoundException();

            var list = entity.Select(x => new MerchantUserDto
            {
                Claims = x.Claims,
                Email = x.Email,
                IsActive = x.IsActive,
                Phone = x.Phone,
                FirstName = x.FirstName,
                LastName = x.LastName
            });

            return new BaseListResponseModel<MerchantUserDto>(request.Limit, list);
        }
    }
}
