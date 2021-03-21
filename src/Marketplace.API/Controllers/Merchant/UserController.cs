using System;
using System.Linq;
using System.Threading.Tasks;
using Marketplace.Application.MerchantServices.MerchantUsers;
using Marketplace.Application.MerchantServices.MerchantUsers.Commands.CreateMerchantUser;
using Marketplace.Application.MerchantServices.MerchantUsers.Commands.DeleteMerchantUser;
using Marketplace.Application.MerchantServices.MerchantUsers.Commands.UpdateMerchantUser;
using Marketplace.Application.MerchantServices.MerchantUsers.Queries.GetMerchantUser;
using Marketplace.Application.MerchantServices.MerchantUsers.Queries.GetMerchantUsers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Marketplace.API.Controllers.Merchant
{
    [Route("v1/merchant/users")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> RegisterUser([FromBody] MerchantUserReq req)
        {
            var merchantCode = Guid.Parse(User.Claims.FirstOrDefault(x => x.Type == "MerchantCode").Value);
            var merchantName = User.Claims.FirstOrDefault(x => x.Type == "MerchantName").Value;
            var response = await _mediator.Send(new CreateMerchantUserCommand(merchantCode, merchantName, req.FirstName, req.LastName, req.Password,
                req.Phone, req.Email, req.IsActive, req.Claims));
            return Created("RegisterUser", response);
        }

        [HttpGet]
        //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<MerchantUserDto>))]
        public async Task<IActionResult> GetUsers([FromQuery] string search, [FromQuery] int limit, [FromQuery] int skip, [FromQuery] string orderBy)
        {
            var merchantCode = Guid.Parse(User.Claims.FirstOrDefault(x => x.Type == "MerchantCode").Value);
            var response = await _mediator.Send(new GetMerchantUsersQuery(merchantCode, search, limit, skip, orderBy));

            return Ok(response);
        }

        [HttpGet("{phoneNumber}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(MerchantUserDto))]
        public async Task<IActionResult> Get([FromRoute] string phoneNumber)
        {
            var merchantCode = Guid.Parse(User.Claims.FirstOrDefault(x => x.Type == "MerchantCode").Value);
            var response = await _mediator.Send(new GetMerchantUserQuery(merchantCode, phoneNumber));

            return Ok(response);
        }

        [HttpPut("{phoneNumber}")]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Update([FromRoute] string phoneNumber, [FromBody] UpdateMerchantUserReq req)
        {
            var merchantCode = Guid.Parse(User.Claims.FirstOrDefault(x => x.Type == "MerchantCode").Value);
            var response = await _mediator.Send(new UpdateMerchantUserCommand(merchantCode, phoneNumber, req.Phone, req.Email, req.FirstName, req.LastName,
                req.Password, req.IsActive, req.Claims));

            if (response)
                return Accepted();
            else
                return Conflict();
        }

        [HttpDelete("{code}")]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Delete([FromRoute] Guid code)
        {
            var merchantCode = Guid.Parse(User.Claims.FirstOrDefault(x => x.Type == "MerchantCode").Value);
            var response = await _mediator.Send(new DeleteMerchantUserCommand(merchantCode, code));
            if (response)
                return Accepted();
            else
                return Conflict();
        }
    }
}
