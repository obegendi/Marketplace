using System;
using System.Collections.Generic;
using Marketplace.Domain.Order;
using Marketplace.Domain.SharedKernel;

namespace Marketplace.Application.OrderServices.CreateOrder
{
    public class CreateOrderReq
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string CustomerCode { get; set; }
        public bool ShouldUpdateCustomer { get; set; }
        public Guid MerchantAddressCode { get; set; }
        public Address ReceiverAddress { get; set; }
        public List<OrderProduct> OrderProducts { get; set; }
        public string OrderNote { get; set; }
        public bool IsContactlessDelivery { get; set; }
    }

}
