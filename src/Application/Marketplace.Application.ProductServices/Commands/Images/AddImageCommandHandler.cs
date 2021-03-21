using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Storage.V1;
using ImageMagick;
using Marketplace.Common.Application.Commands;
using Marketplace.Data.Repositories.Interfaces;
using Marketplace.Domain.Product;
using Microsoft.AspNetCore.Http;

namespace Marketplace.Application.ProductServices.Commands.Images
{
    public class AddImageCommandHandler : ICommandHandler<AddImageCommand, List<Image>>
    {
        private readonly IProductRepository _productRepository;

        public AddImageCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<List<Image>> Handle(AddImageCommand request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetWithSkuAsync(request.Sku);
            if (product is null)
                throw new ProductNotFoundException();
            var list = new List<Image>();
            foreach (var file in request.Files)
            {
                if (product.IsExistImage(file.Name))
                    continue;
                var imageUrl = await ImageProcess(file);
                if (product.AddImage(file.Name, imageUrl))
                    list.Add(new Image { Name = file.Name, Url = imageUrl });
            }
            var result = await _productRepository.PushImageUrls(product);
            return list;
        }

        private async Task<string> ImageProcess(IFormFile file)
        {
            var credential = GoogleCredential.FromFile("./Secrets/b2732904e562.json");
            var storage = await StorageClient.CreateAsync(credential);
            await using (var ms = new MemoryStream())
            {
                file.CopyTo(ms);
                ms.Seek(0, SeekOrigin.Begin);
                var optimizer = new ImageOptimizer();
                optimizer.OptimalCompression = true;

                var r = optimizer.Compress(ms);

                //fileByteArray = ms.ToArray();
                // act on the Base64 data

                using (var image = new MagickImage(ms, new MagickReadSettings()))
                {
                    // Sets the output format to png
                    image.Format = MagickFormat.WebP;

                    // Write the image to the memorystream
                    image.Write(ms);
                    var response = storage.UploadObject("hp-cdn", "images/" + file.Name + "." + MagickFormat.WebP.ToString().ToLower(), "image/webp", ms);
                    return "https://url/" + response.Name;
                }
            }
        }
    }
}