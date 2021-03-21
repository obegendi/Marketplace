using Marketplace.Common.Application.Commands;

namespace Marketplace.Application.OrderServices.CollectOrder
{
    public class CollectOrderCommand : CommandBase
    {
        public CollectOrderCommand(string orderNumber)
        {
            OrderNumber = orderNumber;
        }

        public string OrderNumber { get; }
    }
}
