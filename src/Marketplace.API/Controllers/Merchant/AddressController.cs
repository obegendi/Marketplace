using Marketplace.Application.MerchantServices.MerchantAddresses.Commands.ActivateMerchantAddress;
using Marketplace.Application.MerchantServices.MerchantAddresses.Commands.AddMerchantAddress;
using Marketplace.Application.MerchantServices.MerchantAddresses.Commands.DeactivateMerchantAddress;
using Marketplace.Application.MerchantServices.MerchantAddresses.Commands.DeleteMerchantAddress;
using Marketplace.Application.MerchantServices.MerchantAddresses.Commands.UpdateMerchantAddress;
using Marketplace.Application.MerchantServices.MerchantAddresses.Queries.GetMerchantAddress;
using Marketplace.Application.MerchantServices.MerchantAddresses.Queries.GetMerchantAddressList;
using Marketplace.Domain.SharedKernel;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Marketplace.Application.MerchantServices.AvailableLocations.Commands.ActivateAvailableLocation;
using Marketplace.Application.MerchantServices.AvailableLocations.Commands.AddAvailableLocation;
using Marketplace.Application.MerchantServices.AvailableLocations.Commands.DeactivateAvailableLocation;
using Marketplace.Application.MerchantServices.AvailableLocations.Commands.RemoveAvailableLocation;
using Marketplace.Application.MerchantServices.AvailableLocations.Queries.GetAvailableLocations;

namespace Marketplace.API.Controllers.Merchant
{
    [Route("v1/merchant/addresses")]
    [ApiController]
    [Authorize]
    public class AddressController : ControllerBase
    {
        private readonly IMediator _mediator;
        private Guid merchantCode;
        private string merchantName;

        public AddressController(IMediator mediator)
        {
            _mediator = mediator;

        }

        [HttpGet("{merchantAddressCode}")]
        public async Task<IActionResult> Get([FromRoute] Guid merchantAddressCode)
        {
            merchantCode = Guid.Parse(User.Claims.FirstOrDefault(x => x.Type == "MerchantCode").Value);

            var response = await _mediator.Send(new GetMerchantAddressQuery(merchantCode, merchantAddressCode));
            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> List([FromQuery] int skip, [FromQuery] int limit, [FromQuery] string search, [FromQuery] string orderBy)
        {
            merchantCode = Guid.Parse(User.Claims.FirstOrDefault(x => x.Type == "MerchantCode").Value);

            var response = await _mediator.Send(new GetMerchantAddressListQuery(merchantCode, search, skip, limit, orderBy));
            return Ok(response);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Create([FromBody] MerchantAddressAddReq req)
        {
            merchantCode = Guid.Parse(User.Claims.FirstOrDefault(x => x.Type == "MerchantCode").Value);
            merchantName = User.Claims.FirstOrDefault(x => x.Type == "MerchantName").Value;
            var response = await _mediator.Send(new AddMerchantAddressCommand(merchantCode, merchantName, req.Name, req.Address, req.Country, req.City,
                req.Town, req.District,
                req.Location, req.WorkingHours, req.ExtraInfo, req.IsActive));
            return Created("AddLocation", response);
        }

        [HttpPut("{merchantAddressCode}")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Update([FromRoute] Guid merchantAddressCode, [FromBody] UpdateMerchantAddressReq req)
        {
            merchantCode = Guid.Parse(User.Claims.FirstOrDefault(x => x.Type == "MerchantCode").Value);
            merchantName = User.Claims.FirstOrDefault(x => x.Type == "MerchantName").Value;
            var response = await _mediator.Send(new UpdateMerchantAddressCommand(merchantCode, merchantAddressCode, merchantName, req.Name, req.Address,
                req.City, req.Town, req.District, req.Location, req.WorkingHours, req.ExtraInfo, req.IsActive));
            return Accepted(response);
        }

        [HttpPut("{merchantAddressCode}/deactivate")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Deactivate([FromRoute] Guid merchantAddressCode)
        {
            merchantCode = Guid.Parse(User.Claims.FirstOrDefault(x => x.Type == "MerchantCode").Value);
            merchantName = User.Claims.FirstOrDefault(x => x.Type == "MerchantName").Value;
            await _mediator.Send(new DeactivateMerchantAddressCommand(merchantCode, merchantAddressCode));
            return Accepted();
        }

        [HttpPut("{merchantAddressCode}/activate")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Activate([FromRoute] Guid merchantAddressCode)
        {
            merchantCode = Guid.Parse(User.Claims.FirstOrDefault(x => x.Type == "MerchantCode").Value);
            await _mediator.Send(new ActivateMerchantAddressCommand(merchantCode, merchantAddressCode));
            return Accepted();
        }

        [HttpDelete("{merchantAddressCode}")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Delete([FromRoute] Guid merchantAddressCode)
        {
            merchantCode = Guid.Parse(User.Claims.FirstOrDefault(x => x.Type == "MerchantCode").Value);

            await _mediator.Send(new DeleteMerchantAddressCommand(merchantCode, merchantAddressCode));

            return Accepted();
        }

        [HttpGet("{merchantAddressCode}/availableLocations")]
        public async Task<IActionResult> GetAvailableLocations([FromRoute] Guid merchantAddressCode, [FromQuery] int skip, [FromQuery] int limit, [FromQuery] string search, [FromQuery] string orderBy)
        {
            merchantCode = Guid.Parse(User.Claims.FirstOrDefault(x => x.Type == "MerchantCode").Value);

            var response = await _mediator.Send(new GetAvailableLocationsQuery(merchantCode, merchantAddressCode, search, skip, limit, orderBy));
            return Ok(response);
        }

        [HttpPost("{merchantAddressCode}/availableLocations")]
        public async Task<IActionResult> AddAvailableLocations([FromRoute] Guid merchantAddressCode, [FromBody] List<Location> locations)
        {
            merchantCode = Guid.Parse(User.Claims.FirstOrDefault(x => x.Type == "MerchantCode").Value);

            var response = await _mediator.Send(new AddAvailableLocationCommand(merchantCode, merchantAddressCode, locations));
            return Ok(response);
        }

        [HttpPut("{merchantAddressCode}/availableLocations/active")]
        public async Task<IActionResult> ActiveAvailableLocations([FromRoute] Guid merchantAddressCode, [FromBody] List<Location> locations)
        {
            merchantCode = Guid.Parse(User.Claims.FirstOrDefault(x => x.Type == "MerchantCode").Value);

            var response = await _mediator.Send(new ActivateAvailableLocationCommand(merchantCode, merchantAddressCode, locations));
            return Ok(response);
        }

        [HttpPut("{merchantAddressCode}/availableLocations/deactive")]
        public async Task<IActionResult> DeactiveAvailableLocations([FromRoute] Guid merchantAddressCode, [FromBody] List<Location> locations)
        {
            merchantCode = Guid.Parse(User.Claims.FirstOrDefault(x => x.Type == "MerchantCode").Value);

            var response = await _mediator.Send(new DeactivateAvailableLocationCommand(merchantCode, merchantAddressCode, locations));
            return Ok(response);
        }

        [HttpDelete("{merchantAddressCode}/availableLocations")]
        public async Task<IActionResult> RemoveAvailableLocations([FromRoute] Guid merchantAddressCode, [FromBody] List<Location> locations)
        {
            merchantCode = Guid.Parse(User.Claims.FirstOrDefault(x => x.Type == "MerchantCode").Value);

            var response = await _mediator.Send(new RemoveAvailableLocationCommand(merchantCode, merchantAddressCode, locations));
            return Ok(response);
        }
    }
}
