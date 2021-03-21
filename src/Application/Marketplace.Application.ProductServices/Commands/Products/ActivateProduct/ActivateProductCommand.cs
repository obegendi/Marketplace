using Marketplace.Common.Application.Commands;

namespace Marketplace.Application.ProductServices.Commands.Products.ActivateProduct
{
    public class ActivateProductCommand : CommandBase
    {
        public ActivateProductCommand(string sku)
        {
            Sku = sku;
        }

        public string Sku { get; }
    }
}
