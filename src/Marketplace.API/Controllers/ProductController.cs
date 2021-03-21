using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Marketplace.Application.ProductServices.Commands.Images;
using Marketplace.Application.ProductServices.Commands.Products.ActivateProduct;
using Marketplace.Application.ProductServices.Commands.Products.CreateProduct;
using Marketplace.Application.ProductServices.Commands.Products.DeactivateProduct;
using Marketplace.Application.ProductServices.Commands.Products.DeleteProduct;
using Marketplace.Application.ProductServices.Commands.Products.UpdateProduct;
using Marketplace.Application.ProductServices.Commands.Tags.AddTagsToProduct;
using Marketplace.Application.ProductServices.Commands.Tags.CreateProductTags;
using Marketplace.Application.ProductServices.Commands.Tags.RemoveTagsFromProduct;
using Marketplace.Application.ProductServices.Queries.GetProducts;
using Marketplace.Application.ProductServices.Queries.GetProductTags;
using Marketplace.Application.ProductServices.Queries.GetProductWithSku;
using Microsoft.Extensions.Configuration;

namespace Marketplace.API.Controllers
{
    [Route("v1/products")]
    [ApiController]
    [Authorize]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IConfiguration _configuration;

        public ProductController(IMediator mediator, IConfiguration configuration)
        {
            _mediator = mediator;
            _configuration = configuration;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Create([FromBody] CreateProductReq req)
        {
            var merchantCode = Guid.Parse(User.Claims.FirstOrDefault(x => x.Type == "MerchantCode").Value);
            await _mediator.Send(new CreateProductCommand(req.Name, req.Barcode, req.Unit, req.Description, req.IsActive, merchantCode.ToString(),
                req.ImageUrls, req.Tags));
            return Created("Create", string.Empty);
        }

        //[HttpPost()]
        //public async Task<IActionResult> GetWithTags([FromBody] ProductTagsReq req)
        //{
        //    var response = await _mediator.Send(new GetProductsWithTagsQuery(req.Tags));
        //    return Ok(response);
        //}

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] int skip, [FromQuery(Name = "tags[]")] string[] tags, [FromQuery] int limit,
            [FromQuery] string search, [FromQuery] string orderBy)
        {
            var response = await _mediator.Send(new GetProductsQuery(search, tags, skip, limit, orderBy));
            return Ok(response);
        }

        [HttpGet("{sku}")]
        public async Task<IActionResult> Get([FromRoute] string sku)
        {
            var response = await _mediator.Send(new GetProductWithSkuQuery(sku));
            return Ok(response);
        }

        [HttpDelete("{sku}")]
        public async Task<IActionResult> Delete([FromRoute] string sku)
        {
            await _mediator.Send(new DeleteProductCommand(sku));
            return Accepted();
        }

        [HttpPut("{sku}")]
        public async Task<IActionResult> Update([FromRoute] string sku, [FromBody] UpdateProductReq req)
        {
            var merchantCode = Guid.Parse(User.Claims.FirstOrDefault(x => x.Type == "MerchantCode").Value);
            await _mediator.Send(new UpdateProductCommand(req.Name, sku, req.Barcode, req.Unit, req.Description, req.IsActive, merchantCode.ToString(),
                req.ImageUrls, req.Tags));
            return Accepted();
        }

        [HttpGet("tags")]
        public async Task<IActionResult> ListTags()
        {
            var response = await _mediator.Send(new GetProductTagsQuery());

            return Ok(response);
        }

        [HttpPost("tags")]
        public async Task<IActionResult> CreateTags([FromBody] List<string> tags)
        {
            var response = await _mediator.Send(new CreateProductTagsCommand(tags));

            return Ok(response);
        }

        [HttpPut("{sku}/tags")]
        public async Task<IActionResult> AddTags([FromRoute] string sku, [FromBody] List<string> tags)
        {
            await _mediator.Send(new AddTagsToProductCommand(sku, tags));
            return Accepted();
        }

        [HttpDelete("{sku}/tags")]
        public async Task<IActionResult> RemoveTags([FromRoute] string sku, [FromBody] List<string> tags)
        {
            var response = await _mediator.Send(new RemoveTagsFromProductCommand(sku, tags));
            if (response)
                return Accepted();
            else
                return Conflict();
        }

        [HttpPost("{sku}/images")]
        [ProducesResponseType(StatusCodes.Status304NotModified)]
        [ProducesResponseType(StatusCodes.Status415UnsupportedMediaType)]
        [ProducesResponseType(StatusCodes.Status413PayloadTooLarge)]
        public async Task<IActionResult> AddImages([FromRoute] string sku)
        {
            foreach (var formFile in Request.Form.Files)
            {
                if (formFile.Length > 10 * 1024 * 1024)
                {
                    return StatusCode(413, $"{formFile.FileName} is too large");
                }

                var contentTypes = _configuration.GetSection("Config")["ContenTypes"].Split(",");
                var contentType = formFile.Headers["Content-Type"].ToString();
                if (!contentTypes.Contains(contentType))
                {
                    return StatusCode(415, $"{contentType} is not supported!");
                }
            }
            var response = await _mediator.Send(new AddImageCommand(sku, base.Request.Form.Files));
            if (response.Any())
                return Created("AddImages", response);
            else
                return StatusCode(304, "Same file names already uploaded");
        }

        [HttpDelete("{sku}/images")]
        public async Task<IActionResult> RemoveImages([FromRoute] string sku)
        {
            return Ok();
        }

        [HttpPut("{sku}/activate")]
        public async Task<IActionResult> Activate([FromRoute] string sku)
        {
            await _mediator.Send(new ActivateProductCommand(sku));
            return Accepted();
        }

        [HttpDelete("{sku}/deactivate")]
        public async Task<IActionResult> Deactive([FromRoute] string sku)
        {
            await _mediator.Send(new DeactivateProductCommand(sku));
            return Accepted();
        }
    }
}

