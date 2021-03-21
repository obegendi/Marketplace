using Marketplace.Common.Application.Commands;

namespace Marketplace.Application.OrderServices.CancelProductItem
{
    public class CancelProductItemCommand : CommandBase
    {
        public CancelProductItemCommand(string orderNumber, string sku)
        {
            OrderNumber = orderNumber;
            Sku = sku;
        }

        public string OrderNumber { get; }
        public string Sku { get; }
    }
}
