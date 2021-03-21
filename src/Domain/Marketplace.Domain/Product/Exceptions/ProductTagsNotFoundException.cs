using System;
using System.Runtime.Serialization;
using Marketplace.Domain.Base;

namespace Marketplace.Domain.Product.Exceptions
{
    [Serializable]
    internal class ProductTagsNotFoundException : DomainException
    {
        public ProductTagsNotFoundException()
        {
        }

        public ProductTagsNotFoundException(string message) : base(message)
        {
        }
    }
}
