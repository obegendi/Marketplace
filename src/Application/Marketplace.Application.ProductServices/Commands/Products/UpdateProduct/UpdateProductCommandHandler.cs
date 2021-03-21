using System.Threading;
using System.Threading.Tasks;
using Marketplace.Common.Application.Commands;
using Marketplace.Data.Repositories.Interfaces;
using MediatR;

namespace Marketplace.Application.ProductServices.Commands.Products.UpdateProduct
{
    public class UpdateProductCommandHandler : ICommandHandler<UpdateProductCommand>
    {
        private readonly IProductRepository _productRepository;

        public UpdateProductCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }


        public async Task<Unit> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetWithSkuAsync(request.Sku);
            if(product is null)
                throw new ProductNotFoundException();

            product.Description = request.Description;
            product.IsActive = request.IsActive;
            product.Name = request.Name;
            product.Tags = request.ProductTags;

            await _productRepository.UpdateAsync(request.Sku, product);

            return Unit.Task.Result;
        }
    }
}
