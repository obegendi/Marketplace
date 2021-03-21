using Marketplace.Application.OrderServices.CancelProductItem;
using Marketplace.Application.OrderServices.CollectOrder;
using Marketplace.Application.OrderServices.CreateOrder;
using Marketplace.Application.OrderServices.DeleteOrder;
using Marketplace.Application.OrderServices.DeliverOrder;
using Marketplace.Application.OrderServices.GetMerchantOrders;
using Marketplace.Application.OrderServices.GetOrder;
using Marketplace.Application.OrderServices.TransitOrder;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Marketplace.API.Controllers
{
    [Route("v1/[controller]")]
    [ApiController]
    [Authorize]
    public class OrdersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrdersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{orderNumber}")]
        public async Task<IActionResult> Get([FromRoute] string orderNumber)
        {
            var response = await _mediator.Send(new GetOrderQuery(orderNumber));
            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> List([FromQuery] int skip, [FromQuery] int limit, [FromQuery] string search, [FromQuery] string orderBy)
        {
            var merchantCode = Guid.Parse(User.Claims.FirstOrDefault(x => x.Type == "MerchantCode").Value);
            var response = await _mediator.Send(new GetMerchantOrdersQuery(merchantCode, search, skip, limit, orderBy));
            return Ok(response);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Create([FromBody] CreateOrderReq req)
        {
            var merchantCode = Guid.Parse(User.Claims.FirstOrDefault(x => x.Type == "MerchantCode").Value);
            await _mediator.Send(new CreateOrderCommand(merchantCode, req.MerchantAddressCode, req.ShouldUpdateCustomer, req.CustomerCode, req.FirstName, req.LastName,
                req.Phone, req.OrderNote, req.IsContactlessDelivery, req.ReceiverAddress, req.OrderProducts));
            return Created("Create", "");
        }

        [HttpPut("{orderNumber}/transit")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Transit([FromRoute] string orderNumber)
        {
            await _mediator.Send(new TransitOrderCommand(orderNumber));
            return Accepted();
        }

        [HttpPut("{orderNumber}/collect")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Collect([FromRoute] string orderNumber)
        {
            await _mediator.Send(new CollectOrderCommand(orderNumber));
            return Accepted();
        }

        [HttpPut("{orderNumber}/{sku}/cancel")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> CancelItem([FromRoute] string orderNumber, [FromRoute] string sku)
        {
            await _mediator.Send(new CancelProductItemCommand(orderNumber, sku));
            return Accepted();
        }

        [HttpDelete("{orderNumber}")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Delete([FromRoute] string orderNumber)
        {
            await _mediator.Send(new DeleteOrderCommand(orderNumber));
            return Accepted();
        }

        [HttpPut("{orderNumber}/deliver")]
        public async Task<IActionResult> Deliver([FromRoute] string orderNumber)
        {
            await _mediator.Send(new DeliverOrderCommand(orderNumber));
            return Ok();
        }
    }
}
