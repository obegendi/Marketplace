using System.Threading;
using System.Threading.Tasks;
using Marketplace.API.Infrastructure;
using Marketplace.Common.Application.Queries;
using Marketplace.Data.Repositories.Interfaces;
using Marketplace.Domain.SharedKernel;

namespace Marketplace.Application.MerchantServices.MerchantCustomers.Queries.GetCustomerAddress
{
    public class
        GetCustomerAddressQueryHandler : IQueryHandler<GetCustomerAddressQuery, BaseListResponseModel<Address>>
    {
        private readonly IMerchantCustomerRepository _merchantCustomerRepository;

        public GetCustomerAddressQueryHandler(IMerchantCustomerRepository merchantCustomerRepository)
        {
            _merchantCustomerRepository = merchantCustomerRepository;
        }
        public async Task<BaseListResponseModel<Address>> Handle(GetCustomerAddressQuery request, CancellationToken cancellationToken)
        {
            var customer = await _merchantCustomerRepository.GetAsync(request.Code);

            return new BaseListResponseModel<Address>(request.Limit, customer.Addresses);
        }
    }
}