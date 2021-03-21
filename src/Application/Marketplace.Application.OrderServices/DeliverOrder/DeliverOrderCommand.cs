using Marketplace.Common.Application.Commands;

namespace Marketplace.Application.OrderServices.DeliverOrder
{
    public class DeliverOrderCommand : CommandBase
    {
        public DeliverOrderCommand(string orderNumber)
        {
            OrderNumber = orderNumber;
        }

        public string OrderNumber { get; }
    }
}
