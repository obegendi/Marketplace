using System.Threading;
using System.Threading.Tasks;
using Marketplace.Common.Application.Commands;
using Marketplace.Data.Repositories.Interfaces;
using MediatR;

namespace Marketplace.Application.ProductServices.Commands.Products.ActivateProduct
{
    public class ActivateProductCommandHandler : ICommandHandler<ActivateProductCommand>
    {
        private readonly IProductRepository _productRepository;

        public ActivateProductCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Unit> Handle(ActivateProductCommand request, CancellationToken cancellationToken)
        {
            await _productRepository.ActivateSkuAsync(request.Sku);

            return Unit.Task.Result;
        }
    }
}
