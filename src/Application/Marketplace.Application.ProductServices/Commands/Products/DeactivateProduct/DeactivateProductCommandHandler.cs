using System.Threading;
using System.Threading.Tasks;
using Marketplace.Common.Application.Commands;
using Marketplace.Data.Repositories.Interfaces;
using MediatR;

namespace Marketplace.Application.ProductServices.Commands.Products.DeactivateProduct
{
    public class DeactivateProductCommandHandler : ICommandHandler<DeactivateProductCommand>
    {
        private readonly IProductRepository _productRepository;

        public DeactivateProductCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Unit> Handle(DeactivateProductCommand request, CancellationToken cancellationToken)
        {
            await _productRepository.DeactivateSkuAsync(request.Sku);

            return Unit.Task.Result;
        }
    }
}
