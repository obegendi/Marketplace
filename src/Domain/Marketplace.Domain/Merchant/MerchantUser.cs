using System;
using System.Collections.Generic;
using Marketplace.Domain.Merchant.Events;
using Marketplace.Domain.Merchant.Rules;
using Marketplace.Domain.Seed;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Marketplace.Domain.Merchant
{
    public class MerchantUser : Entity
    {

        public MerchantUser()
        {

        }

        private MerchantUser(Guid merchantCode, string merchantName, string firstName, string lastName, string password, string email, string phone,
            bool isActive,
            List<string> claims)
        {
            Code = Guid.NewGuid();
            MerchantCode = merchantCode;
            MerchantName = merchantName;
            FirstName = firstName;
            LastName = lastName;
            Password = password;
            Email = email;
            Phone = phone;
            IsActive = isActive;
            CreateDate = DateTime.Now;
            Claims = claims;
            IsDeleted = false;
            AddDomainEvent(new MerchantUserCreatedEvent(this));
        }

        private MerchantUser(Guid merchantCode, string merchantName, string firstName, string lastName, string email, string phone, string password,
            List<string> claims)
        {
            Code = Guid.NewGuid();
            MerchantCode = merchantCode;
            MerchantName = merchantName;
            FirstName = firstName;
            LastName = lastName;
            Password = password;
            Email = email;
            Phone = phone;
            Claims = claims;
            CreateDate = DateTime.Now;
            IsActive = false;
            IsDeleted = false;

            if (claims.Contains("account-admin"))
                IsActive = true;

            AddDomainEvent(new MerchantUserCreatedEvent(this));
        }
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public Guid Code { get; set; }
        public DateTime CreateDate { get; set; }
        public Guid MerchantCode { get; set; }
        public string MerchantName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }

        public List<string> Claims { get; set; }

        public static MerchantUser CreateRegisteredOnNewRegister(Guid merchantCode, string merchantName, string firstName, string lastName, string email,
            string phone,
            string password, List<string> claims, IMerchantUserUniquenessChecker accountUniquenessChecker)
        {
            CheckRule(new MerchantUserMustBeUniqueRule(accountUniquenessChecker, phone));
            return new MerchantUser(merchantCode, merchantName, firstName, lastName, email, phone, password, claims);
        }

        public static MerchantUser CreateRegisteredByMerchant(Guid merchantCode, string merchantName, string firstName, string lastName, string password,
            string email,
            string phone, bool isActive, List<string> claims, IMerchantUserUniquenessChecker accountUniquenessChecker)
        {
            CheckRule(new MerchantUserMustBeUniqueRule(accountUniquenessChecker, phone));
            return new MerchantUser(merchantCode, merchantName, firstName, lastName, password, email, phone, isActive, claims);
        }
    }
}
