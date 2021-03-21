using System.Collections.Generic;
using Marketplace.Common.Application.Commands;

namespace Marketplace.Application.ProductServices.Commands.Tags.CreateProductTags
{
    public class CreateProductTagsCommand : CommandBase
    {
        public CreateProductTagsCommand(List<string> tags)
        {
            Tags = tags;
        }

        public List<string> Tags { get; }
    }
}
