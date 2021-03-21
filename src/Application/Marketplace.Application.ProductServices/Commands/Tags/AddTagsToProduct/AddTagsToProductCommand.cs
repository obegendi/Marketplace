using System.Collections.Generic;
using Marketplace.Common.Application.Commands;

namespace Marketplace.Application.ProductServices.Commands.Tags.AddTagsToProduct
{
    public class AddTagsToProductCommand : CommandBase
    {
        public AddTagsToProductCommand(string sku, List<string> tags)
        {
            Sku = sku;
            Tags = tags;
        }

        public string Sku { get; }
        public List<string> Tags { get; }
    }
}
