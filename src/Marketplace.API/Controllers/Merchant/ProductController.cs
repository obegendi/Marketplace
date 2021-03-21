using Marketplace.Application.MerchantServices;
using Marketplace.Application.MerchantServices.MerchantProducts.Commands.CreateMerchantProduct;
using Marketplace.Application.MerchantServices.MerchantProducts.Commands.UpdateMerchantProduct;
using Marketplace.Application.MerchantServices.MerchantProducts.Queries.GetMerchantProduct;
using Marketplace.Application.MerchantServices.MerchantProducts.Queries.GetMerchantProducts;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Marketplace.API.Infrastructure.Helper;
using Marketplace.Application.MerchantServices.MerchantAddresses.Commands.DeleteMerchantAddress;
using Marketplace.Application.MerchantServices.MerchantProducts.Commands.DeleteMerchantProduct;

namespace Marketplace.API.Controllers.Merchant
{
    [Route("v1/merchant/products")]
    [ApiController]
    [Authorize]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{merchantAddressCode}/{sku}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(200, Type = typeof(IEnumerable<MerchantProductDto>))]
        public async Task<IActionResult> Get([FromRoute] Guid merchantAddressCode, [FromRoute] string sku, [FromQuery] int skip, [FromQuery] int limit)
        {
            var merchantCode = HttpContext.GetMerchantCode();
            var response = await _mediator.Send(new GetMerchantProductQuery(merchantCode, merchantAddressCode, sku));

            return Ok(response);
        }

        [HttpGet()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(200, Type = typeof(IEnumerable<MerchantProductDto>))]
        public async Task<IActionResult> Get([FromQuery] Guid merchantAddressCode, [FromQuery] int skip, [FromQuery] int limit, [FromQuery] string search, [FromQuery] string orderBy)
        {
            var merchantCode = HttpContext.GetMerchantCode();
            var response = await _mediator.Send(new GetMerchantProductsQuery(merchantCode, merchantAddressCode, skip, limit, search, orderBy));

            return Ok(response);
        }

        [HttpPost()]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Create([FromBody] CreateMerchantProductReq req)
        {
            var merchantCode = Guid.Parse(User.Claims.FirstOrDefault(x => x.Type == "MerchantCode").Value);
            await _mediator.Send(new CreateMerchantProductCommand(merchantCode, req.MerchantAddressCode, req.Price, req.Vat, req.PriceWithVat,
                req.IsInfiniteStock, req.Stock, req.IsActive, req.Sku));
            return Created("Create", string.Empty);
        }

        [HttpPost("bulk")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> CreateBulk([FromBody] List<MerchantProductDto> req)
        {
            var merchantCode = Guid.Parse(User.Claims.FirstOrDefault(x => x.Type == "MerchantCode").Value);
            await _mediator.Send(new CreateBulkMerchantProductCommand(req, merchantCode));
            return Created("Create", string.Empty);
        }

        [HttpPut("{merchantAddressCode}/{sku}")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Update([FromRoute] Guid merchantAddressCode, [FromRoute] string sku, [FromBody] UpdateMerchantProductReq req)
        {
            var merchantName = User.Claims.FirstOrDefault(x => x.Type == "MerchantName").Value;
            var merchantCode = Guid.Parse(User.Claims.FirstOrDefault(x => x.Type == "MerchantCode").Value);
            await _mediator.Send(new UpdateMerchantProductCommand(merchantCode, merchantAddressCode, req.Name, req.Price, req.Vat, req.PriceWithVat,
                req.IsInfiniteStock, req.Stock, req.IsActive, sku));

            return Accepted();
        }

        [HttpPut("{merchantAddressCode}/{sku}/stock")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> UpdateStock([FromRoute] Guid merchantAddressCode, [FromRoute] string sku,
            [FromBody] UpdateStockMerchantProductReq req)
        {
            var merchantName = User.Claims.FirstOrDefault(x => x.Type == "MerchantName").Value;
            var merchantCode = Guid.Parse(User.Claims.FirstOrDefault(x => x.Type == "MerchantCode").Value);
            await _mediator.Send(new UpdateStockMerchantProductCommand(merchantCode, merchantAddressCode, sku, req.Stock));

            return Accepted();
        }

        [HttpDelete("{merchantAddressCode}/{sku}")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Delete([FromRoute] Guid merchantAddressCode, [FromRoute] string sku)
        {
            var merchantCode = Guid.Parse(User.Claims.FirstOrDefault(x => x.Type == "MerchantCode").Value);
            var result = await _mediator.Send(new DeleteMerchantProductCommand(merchantCode, merchantAddressCode, sku));
            if (result)
                return Accepted();
            else
                return Conflict();
        }
    }
}
