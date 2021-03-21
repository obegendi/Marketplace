using System;
using Marketplace.Common;

namespace Marketplace.Application.ProductServices
{
    [Serializable]
    internal class ProductTagsNotFoundException : NotFoundException
    {
        public ProductTagsNotFoundException()
        {
        }

        public ProductTagsNotFoundException(string message) : base(message)
        {
        }
    }
}
