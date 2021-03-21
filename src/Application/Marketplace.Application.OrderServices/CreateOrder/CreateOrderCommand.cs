using System;
using System.Collections.Generic;
using Marketplace.Common.Application.Commands;
using Marketplace.Domain.Order;
using Marketplace.Domain.SharedKernel;

namespace Marketplace.Application.OrderServices.CreateOrder
{
    public class CreateOrderCommand : CommandBase
    {
        public CreateOrderCommand(Guid merchantCode,
            Guid merchantAddressCode,
            bool shouldUpdateCustomer,
            string customerCode,
            string firstName,
            string lastName,
            string phone,
            string orderNote,
            bool isContactlessDelivery,
            Address receiverAddress,
            List<OrderProduct> productItems)
        {
            MerchantCode = merchantCode;
            MerchantAddressCode = merchantAddressCode;
            ShouldUpdateCustomer = shouldUpdateCustomer;
            CustomerCode = customerCode;
            FirstName = firstName;
            LastName = lastName;
            Phone = phone;
            OrderNote = orderNote;
            IsContactlessDelivery = isContactlessDelivery;
            ReceiverAddress = receiverAddress;
            OrderProducts = productItems;
        }

        public Guid MerchantCode { get; }
        public Guid MerchantAddressCode { get; }
        public bool ShouldUpdateCustomer { get; }
        public string CustomerCode { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; }
        public string Lastname { get; set; }
        public string Phone { get; set; }
        public Address ReceiverAddress { get; set; }
        public List<OrderProduct> OrderProducts { get; set; }
        public string OrderNote { get; set; }
        public bool IsContactlessDelivery { get; }
    }
}
