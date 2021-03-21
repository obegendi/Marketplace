using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Marketplace.Common.Application.Commands;
using Marketplace.Data.Repositories.Interfaces;
using MediatR;

namespace Marketplace.Application.ProductServices.Commands.Tags.AddTagsToProduct
{
    public class AddTagsToProductCommandHandler : ICommandHandler<AddTagsToProductCommand>
    {
        private readonly IProductRepository _productRepository;
        private readonly IProductTagsRepository _productTagsRepository;

        public AddTagsToProductCommandHandler(IProductRepository productRepository, IProductTagsRepository productTagsRepository)
        {
            _productRepository = productRepository;
            _productTagsRepository = productTagsRepository;
        }

        public async Task<Unit> Handle(AddTagsToProductCommand request, CancellationToken cancellationToken)
        {
            if (request.Tags == null || !request.Tags.Any())
                throw new ProductTagsNotFoundException("At least one product tag should assigned");

            var tags = (await _productTagsRepository.GetAllAsync()).Select(x => x.Name);
            foreach (var tag in request.Tags)
            {
                if (!tags.Any(x => x == tag))
                    throw new ProductTagsNotFoundException($"Product Tag Not Found {tag}");
            }

            var product = await _productRepository.GetWithSkuAsync(request.Sku);
            if (product == null)
                throw new ProductNotFoundException();

            foreach (var item in request.Tags)
                product.Tags.Add(item);

            await _productRepository.UpdateAsync(request.Sku, product);

            return Unit.Task.Result;
        }
    }
}
