using System;
using System.Collections.Generic;
using System.Linq;
using Marketplace.Domain.Merchant.Events;
using Marketplace.Domain.Seed;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Marketplace.Domain.Merchant
{
    public class MerchantAddress : Entity
    {

        private MerchantAddress(Guid merchantCode,
            string merchantName,
            string name,
            string address,
            string country,
            string city,
            string town,
            string district,
            string location,
            string workingHours,
            string extraInfo)
        {
            MerchantCode = merchantCode;
            MerchantName = merchantName;
            Name = name;
            Address = address;
            Country = country;
            City = city;
            Town = town;
            District = district;
            Location = location;
            WorkingHours = workingHours;
            ExtraInfo = extraInfo;
            CreateDate = DateTime.Now;
            LastChangeTime = null;
            Code = Guid.NewGuid();
            AvailableLocations = new List<SharedKernel.Location>();
            AddDomainEvent(new MerchantLocationCreatedEvent(merchantName));
        }
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public Guid Code { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? LastChangeTime { get; set; }
        public Guid MerchantCode { get; set; }
        public string MerchantName { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string Town { get; set; }
        public string District { get; set; }
        public string Location { get; set; }
        public string ExtraInfo { get; set; }
        public string WorkingHours { get; set; }
        public bool IsActive { get; set; }

        public List<SharedKernel.Location> AvailableLocations { get; set; }

        public static MerchantAddress RegisterCreated(Guid merchantCode,
            string merchantName,
            string name,
            string address,
            string country,
            string city,
            string town,
            string district,
            string location,
            string workingHours,
            string extraInfo)
        {
            var merchantLocation = new MerchantAddress(merchantCode, merchantName, name, address, country, city, town, district, location, workingHours,
                extraInfo);

            return merchantLocation;
        }

        public MerchantAddress Activate()
        {
            IsActive = true;
            LastChangeTime = DateTime.UtcNow;
            return this;
        }

        public MerchantAddress Deactive()
        {
            IsActive = false;
            LastChangeTime = DateTime.UtcNow;
            return this;
        }

        public MerchantAddress Update(string name,
            string address,
            string city,
            string town,
            string district,
            string location,
            string workingHours,
            string extraInfo)
        {
            Name = name;
            Address = address;
            Country = "TÜRKİYE";
            City = city;
            Town = town;
            District = district;
            Location = location;
            WorkingHours = workingHours;
            ExtraInfo = extraInfo;
            LastChangeTime = DateTime.UtcNow;
            return this;
        }

        public MerchantAddress AddAvailableLocations(List<SharedKernel.Location> locations)
        {
            if (AvailableLocations is null)
                AvailableLocations = new List<SharedKernel.Location>();
            locations.ForEach(x => x.Status = true);
            this.AvailableLocations.AddRange(locations);
            return this;
        }


        public MerchantAddress ActiveAvailableLocations(List<SharedKernel.Location> requestLocations)
        {
            foreach (var location in AvailableLocations)
            {
                if (requestLocations.Any(x => x.City.Equals(location.City) && x.Town.Equals(location.Town) && x.District.Equals(location.District)))
                    location.Status = true;
            }
            return this;
        }

        public MerchantAddress DeactiveAvailableLocations(List<SharedKernel.Location> requestLocations)
        {
            foreach (var location in AvailableLocations)
            {
                if (requestLocations.Any(x => x.City.Equals(location.City) && x.Town.Equals(location.Town) && x.District.Equals(location.District)))
                    location.Status = false;
            }
            return this;
        }
    }
}
