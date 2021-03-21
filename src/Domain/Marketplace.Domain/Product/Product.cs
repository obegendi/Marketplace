using Marketplace.Domain.Product.Events;
using Marketplace.Domain.Product.Exceptions;
using Marketplace.Domain.Seed;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Marketplace.Domain.Product
{
    [BsonIgnoreExtraElements]
    public class Product : Entity, IAggreagateRoot
    {

        public Product()
        {

        }

        private Product(string name, string barcode, string unit, string description, bool isActive, string createdBy, List<string> imageUrls,
            List<string> productTags)
        {
            if (productTags == null || !productTags.Any())
                throw new ProductTagsNotFoundException("At least one product tag should assigned");

            Name = name;
            Barcode = barcode;
            Unit = unit;
            Tags = productTags;
            Description = description;
            IsActive = isActive;
            CreatedBy = createdBy;
            Images = new List<Image>();
            CreatedBy = string.Empty;
            SkuGenerator();
            AddDomainEvent(new ProductCreatedEvent(this));
        }
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string Name { get; set; }
        public string Sku { get; set; }
        public string Barcode { get; set; }
        public string Unit { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public List<Image> Images { get; set; }
        public List<string> Tags { get; set; }

        public static Product Create(string name, string barcode, string unit, string description, bool isActive, string createdBy, List<string> imageUrls,
            List<string> productTags)
        {
            var product = new Product(name, barcode, unit, description, isActive, createdBy, imageUrls, productTags);

            return product;
        }

        public void AddCreatedBy(string createdBy)
        {
            CreatedBy = createdBy;
        }

        public void AddTag(string productTag)
        {
            Tags.Add(productTag);
        }

        public void RemoveTag(string productTag)
        {
            Tags.Remove(productTag);
        }

        private void SkuGenerator()
        {
            var i = 0;
            foreach (var item in Tags)
            {
                var normalized = StringNormalizer(item.ToUpperInvariant());
                if (normalized.Count() > 2)
                {
                    var firstThreeLetter = normalized.Substring(0, 3);
                    Sku += firstThreeLetter;
                }
                else
                {
                    Sku += normalized;
                }
                Sku += '-';
                i++;
                if (i > 2)
                    break;
            }
            var tempGuid = Guid.NewGuid().ToString();
            Sku += tempGuid.Substring(tempGuid.Length - 6);
            //SubtractiveGenerator gen = new SubtractiveGenerator(292929);
            //this.Sku += gen.last;
        }


        private string StringNormalizer(string text)
        {
            var normalized = text.Normalize(NormalizationForm.FormD);
            var asciiOnly = new string(normalized.Where(c => c < 128).ToArray());
            asciiOnly.Replace("o", "", StringComparison.InvariantCultureIgnoreCase);
            asciiOnly.Replace("i", "", StringComparison.InvariantCultureIgnoreCase);
            return asciiOnly;
        }

        public bool AddImage(string name, string imageUrl)
        {
            if (this.Images is null)
                Images = new List<Image>();
            if (!Images.Any(x => x.Name == name))
            {
                Images.Add(new Image { Name = name, Url = imageUrl });
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool IsExistImage(string fileName)
        {
            if (this.Images.Any(x => x.Name == fileName))
                return true;
            else
                return false;
        }
    }
}
