using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Google.Apis.Storage.v1;
using Google.Apis.Util.Store;
using Marketplace.Common.Application.Commands;
using Marketplace.Domain.Product;
using Microsoft.AspNetCore.Http;

namespace Marketplace.Application.ProductServices.Commands.Images
{
    public class AddImageCommand : ICommand<List<Image>>
    {
        public string Sku { get; set; }
        public IFormFileCollection Files { get; set; }

        public AddImageCommand(string sku, IFormFileCollection files)
        {
            Sku = sku;
            Files = files;
        }

        public Guid Id { get; }
    }
}
