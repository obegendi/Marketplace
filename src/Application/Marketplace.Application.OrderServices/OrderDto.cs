using System;
using System.Collections.Generic;
using Marketplace.Domain.Order;
using Marketplace.Domain.SharedKernel;

namespace Marketplace.Application.OrderServices
{
    public class OrderDto
    {
        public string OrderNumber { get; set; }
        public string MerchantName { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal TotalVat { get; set; }
        public decimal TotalPriceWithVat { get; set; }
        public string FirstName { get; set; }
        public string Lastname { get; set; }
        public string Phone { get; set; }
        public Address SenderAddress { get; set; }
        public Address ReceiverAddress { get; set; }
        public List<OrderProduct> ProductItemList { get; set; }
        public List<OrderProduct> CanceledProductItemList { get; set; }
        public List<State> StateList { get; set; }
    }
}
