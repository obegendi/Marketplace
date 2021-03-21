using System.Threading;
using System.Threading.Tasks;
using Marketplace.Common.Application.Commands;
using Marketplace.Data.Repositories.Interfaces;
using MediatR;

namespace Marketplace.Application.MerchantServices.MerchantProducts.Commands.UpdateMerchantProduct
{
    public class UpdateStockMerchantProductCommandHandler : ICommandHandler<UpdateStockMerchantProductCommand>
    {
        private readonly IMerchantProductRepository _merchantProductRepository;

        public UpdateStockMerchantProductCommandHandler(IMerchantProductRepository merchantProductRepository)
        {
            _merchantProductRepository = merchantProductRepository;
        }

        public async Task<Unit> Handle(UpdateStockMerchantProductCommand request, CancellationToken cancellationToken)
        {
            await _merchantProductRepository.UpdateStockAsync(request.Sku, request.MerchantCode, request.MerchantAddressCode, request.Stock);

            return Unit.Task.Result;
        }
    }
}
