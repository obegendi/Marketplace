using Marketplace.Common.Application.Commands;

namespace Marketplace.Application.ProductServices.Commands.Products.DeactivateProduct
{
    public class DeactivateProductCommand : CommandBase
    {
        public DeactivateProductCommand(string sku)
        {
            Sku = sku;
        }

        public string Sku { get; }
    }
}
