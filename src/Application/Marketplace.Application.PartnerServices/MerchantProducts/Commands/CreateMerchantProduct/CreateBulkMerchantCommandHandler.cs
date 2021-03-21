using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Marketplace.Common.Application.Commands;
using Marketplace.Data.Repositories.Interfaces;
using MediatR;

namespace Marketplace.Application.MerchantServices.MerchantProducts.Commands.CreateMerchantProduct
{
    public class CreateBulkMerchantCommandHandler : ICommandHandler<CreateBulkMerchantProductCommand>
    {
        private readonly IMerchantAddressRepository _merchantAddressRepository;
        private readonly IMerchantProductRepository _merchantProductRepository;
        private readonly IProductRepository _productRepository;

        public CreateBulkMerchantCommandHandler(IMerchantAddressRepository merchantAddressRepository, IMerchantProductRepository merchantProductRepository,
            IProductRepository productRepository)
        {
            _merchantAddressRepository = merchantAddressRepository;
            _merchantProductRepository = merchantProductRepository;
            _productRepository = productRepository;
        }
        public async Task<Unit> Handle(CreateBulkMerchantProductCommand request, CancellationToken cancellationToken)
        {
            var merchantProductList = new List<Domain.Merchant.MerchantProduct>();
            foreach (var item in request.CreateMerchantProductList)
            {
                var product = await _productRepository.GetWithSkuAsync(item.Sku);
                if (product is null)
                    continue;
                var merchantAddress = await _merchantAddressRepository.GetAsync(request.MerchantCode, item.MerchantAddressCode);
                if (merchantAddress is null)
                {
                    throw new MerchantAddressNotFoundException();
                }
                var newMerchantProduct = Domain.Merchant.MerchantProduct.AddCreated(request.MerchantCode,
                    product.Name,
                    product.Sku,
                    product.Barcode,
                    item.Price,
                    item.Vat,
                    item.PriceWithVat,
                    item.IsActive,
                    product.Description,
                    item.Stock,
                    item.IsInfiniteStock,
                    merchantAddress.Code,
                    product.Images,
                    product.Tags);
                merchantProductList.Add(newMerchantProduct);
            }
            await _merchantProductRepository.BulkCreateAsync(merchantProductList);

            return Unit.Task.Result;
        }
    }
}
