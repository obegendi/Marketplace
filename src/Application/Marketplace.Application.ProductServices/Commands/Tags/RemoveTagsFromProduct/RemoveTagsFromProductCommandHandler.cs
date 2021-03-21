using System.Threading;
using System.Threading.Tasks;
using Marketplace.Common.Application.Commands;
using Marketplace.Data.Repositories.Interfaces;

namespace Marketplace.Application.ProductServices.Commands.Tags.RemoveTagsFromProduct
{
    public class RemoveTagsFromProductCommandHandler : ICommandHandler<RemoveTagsFromProductCommand, bool>
    {
        private readonly IProductRepository _productRepository;

        public RemoveTagsFromProductCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;

        }

        /// <inheritdoc />
        public async Task<bool> Handle(RemoveTagsFromProductCommand request, CancellationToken cancellationToken)
        {
            var response = await _productRepository.RemoveTags(request.Sku, request.Tags);

            return response;
        }
    }
}
