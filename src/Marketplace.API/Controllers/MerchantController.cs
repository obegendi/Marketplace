using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Marketplace.Application.MerchantServices.AvailableLocations.Commands.ActivateAvailableLocation;
using Marketplace.Application.MerchantServices.AvailableLocations.Commands.AddAvailableLocation;
using Marketplace.Application.MerchantServices.AvailableLocations.Commands.DeactivateAvailableLocation;
using Marketplace.Domain.SharedKernel;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Marketplace.API.Controllers
{
    [Route("v1/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class MerchantController : ControllerBase
    {
        private readonly IMediator _mediator;
        private Guid merchantCode;

        public MerchantController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPut("{merchantAddressCode}/active")]
        public async Task<IActionResult> ActivateAvailableLocation([FromRoute] Guid merchantAddressCode, [FromBody] List<Location> req)
        {
            merchantCode = Guid.Parse(User.Claims.FirstOrDefault(x => x.Type == "MerchantCode").Value);
            await _mediator.Send(new ActivateAvailableLocationCommand(merchantCode, merchantAddressCode, req));
            return Accepted();
        }

        [HttpPut("{merchantAddressCode}/deactive")]
        public async Task<IActionResult> DeactivateAvailableLocation([FromRoute] Guid merchantAddressCode, [FromBody] List<Location> req)
        {
            merchantCode = Guid.Parse(User.Claims.FirstOrDefault(x => x.Type == "MerchantCode").Value);
            await _mediator.Send(new DeactivateAvailableLocationCommand(merchantCode, merchantAddressCode, req));
            return Accepted();
        }

        [HttpPost("{merchantAddressCode}")]
        public async Task<IActionResult> AddAvailableLocation([FromRoute] Guid merchantAddressCode, [FromBody] List<Location> req)
        {
            merchantCode = Guid.Parse(User.Claims.FirstOrDefault(x => x.Type == "MerchantCode").Value);
            await _mediator.Send(new AddAvailableLocationCommand(merchantCode, merchantAddressCode, req));
            return Created("AddAvailableLocation", string.Empty);
        }

        [HttpDelete("{merchantAddressCode}")]
        public async Task<IActionResult> DeleteAvailableLocation([FromRoute] Guid merchantAddressCode, [FromBody] List<Location> req)
        {
            merchantCode = Guid.Parse(User.Claims.FirstOrDefault(x => x.Type == "MerchantCode").Value);
            await _mediator.Send(new AddAvailableLocationCommand(merchantCode, merchantAddressCode, req));
            return Created("AddAvailableLocation", string.Empty);
        }
    }
}
