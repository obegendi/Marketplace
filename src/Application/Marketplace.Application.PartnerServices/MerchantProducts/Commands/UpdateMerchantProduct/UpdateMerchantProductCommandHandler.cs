using System.Threading;
using System.Threading.Tasks;
using Marketplace.Application.MerchantServices.MerchantProducts.Commands.CreateMerchantProduct;
using Marketplace.Common.Application.Commands;
using Marketplace.Data.Repositories.Interfaces;
using MediatR;

namespace Marketplace.Application.MerchantServices.MerchantProducts.Commands.UpdateMerchantProduct
{
    public class UpdateMerchantProductCommandHandler : ICommandHandler<UpdateMerchantProductCommand>
    {
        private readonly IMerchantAddressRepository _merchantAddressRepository;
        private readonly IMerchantProductRepository _merchantProductRepository;
        private readonly IProductRepository _productRepository;

        public UpdateMerchantProductCommandHandler(IProductRepository productRepository, IMerchantAddressRepository merchantAddressRepository,
            IMerchantProductRepository merchantProductRepository)
        {
            _productRepository = productRepository;
            _merchantAddressRepository = merchantAddressRepository;
            _merchantProductRepository = merchantProductRepository;
        }
        public async Task<Unit> Handle(UpdateMerchantProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetWithSkuAsync(request.Sku);
            if (product is null)
                throw new ProductNotFoundException();
            var merchantAddress = await _merchantAddressRepository.GetAsync(request.MerchantCode, request.MerchantAddressCode);
            if (merchantAddress is null)
                throw new MerchantAddressNotFoundException();

            await _merchantProductRepository.UpdateAsync(request.Sku, request.MerchantCode, request.MerchantAddressCode,
                new Domain.Merchant.MerchantProduct
                {
                    IsActive = request.IsActive,
                    IsInfiniteStock = request.IsInfiniteStock,
                    Stock = request.Stock,
                    MerchantAddressCode = merchantAddress.Code,
                    Price = request.Price,
                    Vat = request.Vat,
                    PriceWithVat = request.PriceWithVat,
                    ProductImageUrls = product.Images,
                    ProductTags = product.Tags,
                    Name = request.ProductName
                });

            return Unit.Task.Result;
        }
    }
}
