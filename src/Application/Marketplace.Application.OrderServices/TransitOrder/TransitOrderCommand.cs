using Marketplace.Common.Application.Commands;

namespace Marketplace.Application.OrderServices.TransitOrder
{
    public class TransitOrderCommand : CommandBase
    {
        public TransitOrderCommand(string orderNumber)
        {
            OrderNumber = orderNumber;
        }

        public string OrderNumber { get; }
    }
}
