using System;
using System.Collections.Generic;
using System.Linq;
using Marketplace.Common;
using Marketplace.Domain.Order.Events;
using Marketplace.Domain.Order.Exceptions;
using Marketplace.Domain.Seed;
using Marketplace.Domain.SharedKernel;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Marketplace.Domain.Order
{
    public class Order : Entity
    {

        private Order(string merchantName,
            Guid merchantCode,
            string firstName,
            string lastName,
            string phone,
            string orderNote,
            Address senderAddress,
            Address receiverAddress,
            List<OrderProduct> productItems)
        {

            if (!productItems.Any())
                throw new OrderProductListNotContainAnyProductBusinessException();

            MerchantName = merchantName;
            MerchantCode = merchantCode;
            FirstName = firstName;
            LastName = lastName;
            Phone = phone;
            OrderNote = orderNote;
            SenderAddress = senderAddress;
            ReceiverAddress = receiverAddress;
            OrderProducts = productItems;
            CanceledOrderProducts = new List<OrderProduct>();

            OrderNumber = Guid.NewGuid().ToString().Split('-')[0];

            TotalPriceCalculator(productItems);

            States = new List<State> { new State { StateName = Consts.Order.Created, CreatedDate = DateTime.Now } };

            AddDomainEvent(new OrderCreatedEvent(this));
        }
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string OrderNumber { get; set; }
        public DateTime CreateDate { get; set; }
        public string MerchantName { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal TotalVat { get; set; }
        public decimal TotalPriceWithVat { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string OrderNote { get; set; }
        public Address SenderAddress { get; set; }
        public Address ReceiverAddress { get; set; }
        public List<OrderProduct> OrderProducts { get; set; }
        public List<OrderProduct> CanceledOrderProducts { get; set; }
        public List<State> States { get; set; }
        public Guid MerchantCode { get; set; }

        public Order StartCollect()
        {
            if (!States.Any())
                throw new OrderStatesNotFoundBusinessException();

            var stateName = States.OrderByDescending(x => x.CreatedDate).FirstOrDefault().StateName;

            if (stateName != Consts.Order.Created)
                throw new OrderStateIsNotValidBusinessException();

            States.Add(new State { CreatedDate = DateTime.Now, StateName = Consts.Order.Collecting });

            return this;
        }

        public Order Deliver()
        {
            if (!States.Any())
                throw new OrderStatesNotFoundBusinessException();

            var stateName = States.OrderByDescending(x => x.CreatedDate).FirstOrDefault().StateName;

            if (stateName != Consts.Order.InTransit)
                throw new OrderStateIsNotValidBusinessException();

            States.Add(new State { CreatedDate = DateTime.Now, StateName = Consts.Order.Delivered });

            return this;
        }

        public Order InTransit()
        {
            if (!States.Any())
                throw new OrderStatesNotFoundBusinessException();

            var stateName = States.OrderByDescending(x => x.CreatedDate).FirstOrDefault().StateName;

            if (stateName != Consts.Order.Collecting)
                throw new OrderStateIsNotValidBusinessException();

            States.Add(new State { CreatedDate = DateTime.Now, StateName = Consts.Order.InTransit });

            return this;
        }

        public static Order Create(string merchantName,
            Guid merchantCode,
            string firstName,
            string lastName,
            string phone,
            string orderNote,
            Address senderAddress,
            Address receiverAddress,
            List<OrderProduct> productItems)
        {
            return new Order(merchantName, merchantCode, firstName, lastName, phone, orderNote, senderAddress, receiverAddress, productItems);
        }

        public Order AddProduct(OrderProduct product)
        {
            OrderProducts.Add(product);
            TotalPrice += product.Price * product.Quantity;
            TotalPriceWithVat += product.PriceWithVat * product.Quantity;
            TotalVat += product.Vat * product.Quantity;

            return this;
        }

        public Order CancelLineItem(string sku)
        {
            if (States.Count > 1)
                throw new CannotCancelOrderProductItemException();

            var product = OrderProducts.FirstOrDefault(x => x.Sku == sku);
            OrderProducts.Remove(product);
            CanceledOrderProducts.Add(product);

            TotalPrice -= product.Price * product.Quantity;
            TotalPriceWithVat -= product.PriceWithVat * product.Quantity;
            TotalVat -= product.Vat * product.Quantity;

            return this;
        }

      

        public Order Change(List<OrderProduct> productList)
        {
            foreach (var product in productList)
            {
                var selectedProduct = OrderProducts.FirstOrDefault(x => x.Sku == product.Sku);
                if (product.Quantity - selectedProduct.Quantity < 1)
                    throw new ProductQuantityMustBeGreaterThanZeroBusinessException();

                selectedProduct.Quantity = product.Quantity;
            }
            TotalPriceCalculator(productList);
            return this;
        }

        public Order UpdateOrderNote(string orderNote)
        {
            if (States.Count > 1)
                throw new CannotUpdateOrderNoteException();
            OrderNote = orderNote;
            return this;
        }

        private void TotalPriceCalculator(List<OrderProduct> orderProductList)
        {
            foreach (var product in orderProductList)
            {
                TotalPrice += product.Price * product.Quantity;
                TotalPriceWithVat += product.PriceWithVat * product.Quantity;
                TotalVat += product.Vat * product.Quantity;
            }
        }
    }
}
