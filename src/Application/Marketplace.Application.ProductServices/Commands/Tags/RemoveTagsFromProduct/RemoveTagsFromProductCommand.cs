using System.Collections.Generic;
using Marketplace.Common.Application.Commands;

namespace Marketplace.Application.ProductServices.Commands.Tags.RemoveTagsFromProduct
{
    public class RemoveTagsFromProductCommand : CommandBase<bool>
    {
        public string Sku { get; }
        public List<string> Tags { get; }
        public RemoveTagsFromProductCommand(string sku, List<string> tags)
        {
            Sku = sku;
            Tags = tags;
        }
    }

}
