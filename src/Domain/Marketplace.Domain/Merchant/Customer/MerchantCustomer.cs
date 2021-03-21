using Marketplace.Domain.Seed;
using Marketplace.Domain.SharedKernel;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Marketplace.Domain.Merchant.Customer
{
    public class MerchantCustomer : Entity, IAggreagateRoot
    {

        public MerchantCustomer()
        {

        }

        private MerchantCustomer(Guid merchantCode, string email, string phone, string firstName, string lastName, string createdBy, List<Address> addresses)
        {
            MerchantCode = merchantCode;
            Email = email;
            Phone = phone;
            FirstName = firstName;
            LastName = lastName;
            Code = Guid.NewGuid();
            CreateDate = DateTime.UtcNow;
            CreatedBy = createdBy;
            if (addresses.Any())
            {
                foreach (var address in addresses)
                {
                    AddAddress(address);
                }
            }
            else
                Addresses = new List<Address>();

            AddDomainEvent(new MerchantCustomerRegistered(this));
        }
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public Guid Code { get; set; }
        public Guid MerchantCode { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdateBy { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<Address> Addresses { get; set; }

        public MerchantCustomer AddAddress(Address address)
        {
            address.Code = Guid.NewGuid();
            if (Addresses == null)
                Addresses = new List<Address>();
            Addresses.Add(address);
            return this;
        }

        public static MerchantCustomer CreateNew(Guid merchantCode, string email, string phone, string firstName, string lastName, string createdBy, List<Address> addresses)
        {
            return new MerchantCustomer(merchantCode, email, phone, firstName, lastName, createdBy, addresses);
        }

        public MerchantCustomer UpdateAddress(Guid code, string name, string city, string town, string district, string fullAddress, string updateBy)
        {
            var address = Addresses.FirstOrDefault(x => x.Code == code);
            if (address is null)
                throw new CustomerAddressNotFound();

            address.Name = name;
            address.City = city;
            address.Town = town;
            address.District = district;
            address.FullAddress = fullAddress;

            this.UpdateDate = DateTime.UtcNow;
            this.UpdateBy = updateBy;

            return this;
        }

        public MerchantCustomer DeleteAddress(Guid code, string updateBy)
        {
            var address = Addresses.FirstOrDefault(x => x.Code == code);
            if (address is null)
                throw new CustomerAddressNotFound();

            this.UpdateDate = DateTime.UtcNow;
            this.UpdateBy = updateBy;
            Addresses.Remove(address);

            return this;
        }

        public MerchantCustomer Update(string email, string phone, string firstName, string lastName, string updateBy, List<Address> addresses)
        {
            this.Email = email;
            this.Phone = phone;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.UpdateBy = updateBy;
            this.UpdateDate = DateTime.UtcNow;
            if (addresses.Any())
                foreach (var address in addresses)
                {
                    if (Addresses == null || !Addresses.Any(x => x.Name == address.Name))
                        AddAddress(address);
                }

            return this;
        }
    }
}
