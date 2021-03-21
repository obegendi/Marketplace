using System.Threading;
using System.Threading.Tasks;
using Marketplace.Common.Application.Commands;
using Marketplace.Data.Repositories.Interfaces;
using MediatR;

namespace Marketplace.Application.MerchantServices.MerchantProducts.Commands.CreateMerchantProduct
{
    public class CreateMerchantProductCommandHandler : ICommandHandler<CreateMerchantProductCommand>
    {
        private readonly IMerchantAddressRepository _merchantAddressRepository;
        private readonly IMerchantProductRepository _merchantProductRepository;
        private readonly IProductRepository _productRepository;

        public CreateMerchantProductCommandHandler(IProductRepository productRepository, IMerchantProductRepository merchantProductRepository,
            IMerchantAddressRepository merchantAddressRepository)
        {
            _productRepository = productRepository;
            _merchantProductRepository = merchantProductRepository;
            _merchantAddressRepository = merchantAddressRepository;
        }

        public async Task<Unit> Handle(CreateMerchantProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetWithSkuAsync(request.Sku);
            if (product == null)
                throw new ProductNotFoundException("Product Not Found");
            var merchantAddress = await _merchantAddressRepository.GetAsync(request.MerchantCode, request.MerchantAddressCode);
            if(merchantAddress is null)
                throw new MerchantAddressNotFoundException();

            var newMerchantProduct = Domain.Merchant.MerchantProduct.AddCreated(request.MerchantCode,
                product.Name,
                product.Sku,
                product.Barcode,
                request.Price,
                request.Vat,
                request.PriceWithVat,
                request.IsActive,
                product.Description,
                request.Stock,
                request.IsInfiniteStock,
                request.MerchantAddressCode,
                product.Images,
                product.Tags);

            await _merchantProductRepository.CreateAsync(newMerchantProduct);

            return Unit.Task.Result;
        }
    }
}
