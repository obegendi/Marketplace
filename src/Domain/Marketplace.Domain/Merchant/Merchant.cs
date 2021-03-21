using System;
using System.Collections.Generic;
using System.Linq;
using Marketplace.Domain.Merchant.Events;
using Marketplace.Domain.Merchant.Exceptions;
using Marketplace.Domain.Seed;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Marketplace.Domain.Merchant
{
    public class Merchant : Entity, IAggreagateRoot
    {

        public Merchant()
        {

        }
        private Merchant(string name)
        {
            Name = name;
            AddDomainEvent(new MerchantRegisteredEvent(Name));
        }
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public Guid Code { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }

        public List<string> ActiveAddressNames { get; set; }

        public static Merchant Register(string name)
        {
            var merchant = new Merchant(name)
            {
                Code = Guid.NewGuid(),
                ActiveAddressNames = new List<string>(),
                IsActive = true,
                IsDeleted = false
            };
            return merchant;
        }

        public void DeleteMerchant()
        {
            IsActive = false;
            IsDeleted = true;
        }

        public void DeactivateMerchant()
        {
            IsActive = false;
        }

        public void AddActiveAddress(string addressName)
        {
            ActiveAddressNames.Add(addressName);
        }

        public void RemoveActiveAddress(string addressName)
        {
            if (!ActiveAddressNames.Any(x => x == addressName))
                throw new ActiveAddressNameNotfoundException();
            ActiveAddressNames.Remove(addressName);
        }
    }
}
