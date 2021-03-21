using System.Threading;
using System.Threading.Tasks;
using Marketplace.Common.Application.Commands;
using Marketplace.Data.Repositories.Interfaces;

namespace Marketplace.Application.MerchantServices.MerchantProducts.Commands.DeleteMerchantProduct
{
    public class DeleteMerchantProductCommandHandler : ICommandHandler<DeleteMerchantProductCommand, bool>
    {
        private readonly IMerchantProductRepository _merchantProductRepository;

        public DeleteMerchantProductCommandHandler(IMerchantProductRepository merchantProductRepository)
        {
            _merchantProductRepository = merchantProductRepository;
        }

        public Task<bool> Handle(DeleteMerchantProductCommand request, CancellationToken cancellationToken)
        {
            var result = _merchantProductRepository.DeleteAsync(request.Sku, request.MerchantCode, request.MerchantAddressCode);

            return result;
        }
    }
}