using Marketplace.Common.Application.Queries;

namespace Marketplace.Application.OrderServices.GetOrder
{
    public class GetOrderQuery : IQuery<OrderDto>
    {
        public GetOrderQuery(string orderNumber)
        {
            OrderNumber = orderNumber;
        }

        public string OrderNumber { get; }
    }
}
