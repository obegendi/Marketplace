using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Marketplace.Common.Application.Commands;
using Marketplace.Data.Repositories.Interfaces;
using Marketplace.Domain.Product;
using MediatR;

namespace Marketplace.Application.ProductServices.Commands.Tags.CreateProductTags
{
    public class CreateProductTagsCommandHandler : ICommandHandler<CreateProductTagsCommand>
    {
        private readonly IProductTagsRepository _productTagsRepository;

        public CreateProductTagsCommandHandler(IProductTagsRepository productTagsRepository)
        {
            _productTagsRepository = productTagsRepository;
        }

        public async Task<Unit> Handle(CreateProductTagsCommand request, CancellationToken cancellationToken)
        {
            var tagList = new List<ProductTag>();
            foreach (var item in request.Tags)
                tagList.Add(new ProductTag { Name = item });
            await _productTagsRepository.CreateAsync(tagList);

            return Unit.Task.Result;
        }
    }
}
