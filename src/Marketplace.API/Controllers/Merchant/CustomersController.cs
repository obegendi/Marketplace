using System;
using System.Linq;
using System.Threading.Tasks;
using Marketplace.Application.MerchantServices.MerchantCustomers.Commands.AddAddress;
using Marketplace.Application.MerchantServices.MerchantCustomers.Commands.DeleteCustomer;
using Marketplace.Application.MerchantServices.MerchantCustomers.Commands.DeleteCustomerAddress;
using Marketplace.Application.MerchantServices.MerchantCustomers.Commands.RegisterCustomer;
using Marketplace.Application.MerchantServices.MerchantCustomers.Commands.UpdateCustomer;
using Marketplace.Application.MerchantServices.MerchantCustomers.Commands.UpdateCustomerAddress;
using Marketplace.Application.MerchantServices.MerchantCustomers.Queries.GetCustomer;
using Marketplace.Application.MerchantServices.MerchantCustomers.Queries.GetCustomerAddress;
using Marketplace.Application.MerchantServices.MerchantCustomers.Queries.GetCustomers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Marketplace.API.Controllers.Merchant
{
    [Route("v1/merchant/[controller]")]
    [ApiController]
    [Authorize]
    public class CustomersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CustomersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{code}")]
        public async Task<IActionResult> Get([FromRoute] Guid code)
        {
            var response = await _mediator.Send(new GetCustomerQuery(code));
            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetCustomers([FromQuery] int skip, [FromQuery] int limit, [FromQuery] string search, [FromQuery] string orderBy)
        {
            var merchantCode = Guid.Parse(User.Claims.FirstOrDefault(x => x.Type == "MerchantCode").Value);
            var response = await _mediator.Send(new GetCustomersQuery(merchantCode, search, skip, limit, orderBy));

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterCustomerReq req)
        {
            var merchantCode = Guid.Parse(User.Claims.FirstOrDefault(x => x.Type == "MerchantCode").Value);
            var merchantName = User.Claims.FirstOrDefault(x => x.Type == "MerchantName").Value;
            await _mediator.Send(new RegisterCustomerCommand(merchantCode, req.Email, req.Phone, req.FirstName, req.LastName, merchantName, req.Addresses));
            return CreatedAtAction("Register", string.Empty);
        }

        [HttpPut("{code}")]
        public async Task<IActionResult> Update([FromRoute] Guid code, [FromBody] UpdateCustomerReq req)
        {
            var merchantCode = Guid.Parse(User.Claims.FirstOrDefault(x => x.Type == "MerchantCode").Value);
            var merchantName = User.Claims.FirstOrDefault(x => x.Type == "MerchantName").Value;
            var response = await _mediator.Send(new UpdateCustomerCommand(merchantCode, code, req.Email, req.Phone, req.FirstName, req.LastName, merchantName, req.Addresses));
            if (response != null)
                return Accepted(response);
            else
                return Conflict();
        }

        [HttpDelete("{code}")]
        public async Task<IActionResult> Delete([FromRoute] Guid code)
        {
            var response = await _mediator.Send(new DeleteCustomerCommand(code));
            if (response)
                return Accepted();
            else
                return Conflict();
        }

        [HttpGet("{code}/address")]
        public async Task<IActionResult> GetAddress([FromRoute] Guid code, [FromQuery] int skip, [FromQuery] int limit, [FromQuery] string search, [FromQuery] string orderBy)
        {
            var response = _mediator.Send(new GetCustomerAddressQuery(code, search, skip, limit, orderBy));
            return Ok(response);
        }

        [HttpPost("{code}/addresses")]
        public async Task<IActionResult> AddAddress([FromRoute] Guid code, [FromBody] AddAddressReq req)
        {
            await _mediator.Send(new AddAddressCommand(code, req.Name, req.City, req.Town, req.District, req.FullAddress));
            return CreatedAtAction("AddAddress", string.Empty);
        }

        [HttpPut("{code}/addresses/{addressCode}")]
        public async Task<IActionResult> UpdateCustomerAddress([FromRoute] Guid code, [FromRoute] Guid addressCode, [FromBody] UpdateCustomerAddressReq req)
        {
            var merchantName = User.Claims.FirstOrDefault(x => x.Type == "MerchantName").Value;
            await _mediator.Send(new UpdateAddressCommand(code, addressCode, req.Email, req.Name, req.City, req.Town, req.District, req.FullAddress, merchantName));
            return Accepted();
        }

        [HttpDelete("{code}/addresses/{addressCode}")]
        public async Task<IActionResult> DeleteCustomerAddress([FromRoute] Guid code, [FromRoute] Guid addressCode)
        {
            var response = await _mediator.Send(new DeleteAddressCommand(code, addressCode));
            if (response)
                return Accepted();
            else
                return Conflict();
        }
    }
}
