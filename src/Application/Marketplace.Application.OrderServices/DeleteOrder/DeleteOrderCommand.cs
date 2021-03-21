using Marketplace.Common.Application.Commands;

namespace Marketplace.Application.OrderServices.DeleteOrder
{
    public class DeleteOrderCommand : CommandBase
    {
        public DeleteOrderCommand(string orderNumber)
        {
            OrderNumber = orderNumber;
        }

        public string OrderNumber { get; }
    }
}
