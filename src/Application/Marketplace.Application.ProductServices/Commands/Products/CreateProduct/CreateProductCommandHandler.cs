using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Marketplace.Common.Application.Commands;
using Marketplace.Data.Repositories.Interfaces;
using Marketplace.Domain.Product;
using MediatR;

namespace Marketplace.Application.ProductServices.Commands.Products.CreateProduct
{
    public class CreateProductCommandHandler : ICommandHandler<CreateProductCommand>
    {
        private readonly IProductRepository _productRepository;
        private readonly IProductTagsRepository _productTagsRepository;

        public CreateProductCommandHandler(IProductRepository productRepository, IProductTagsRepository productTagsRepository)
        {
            _productRepository = productRepository;
            _productTagsRepository = productTagsRepository;
        }

        public async Task<Unit> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var tags = (await _productTagsRepository.GetAllAsync()).Select(x => x.Name);
            foreach (var tag in request.Tags)
            {
                if (!tags.Any(x => x == tag))
                    throw new ProductTagsNotFoundException($"Product Tag Not Found {tag}");
            }

            var product = Product.Create(request.Name, request.Barcode, request.Unit, request.Description, request.IsActive, request.CreatedBy,
                request.ImageUrls, request.Tags);
            product.AddCreatedBy(request.CreatedBy);
            await _productRepository.CreateAsync(product);

            return Unit.Task.Result;
        }
    }
}
