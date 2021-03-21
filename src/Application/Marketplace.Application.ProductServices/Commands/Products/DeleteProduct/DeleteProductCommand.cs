using Marketplace.Common.Application.Commands;

namespace Marketplace.Application.ProductServices.Commands.Products.DeleteProduct
{
    public class DeleteProductCommand : CommandBase
    {
        public DeleteProductCommand(string sku)
        {
            Sku = sku;
        }

        public string Sku { get; }
    }
}
