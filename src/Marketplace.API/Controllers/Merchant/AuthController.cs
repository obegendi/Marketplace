using System.Threading.Tasks;
using Marketplace.Application.AuthenticationServices;
using Marketplace.Application.MerchantServices.MerchantUsers.Commands.ForgotPassword;
using Marketplace.Application.MerchantServices.MerchantUsers.Commands.RegisterMerchantUser;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Marketplace.API.Controllers.Merchant
{
    [Route("v1/merchant/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator, IAuthenticationService authenticationService)
        {
            _mediator = mediator;
            _authenticationService = authenticationService;
        }

        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesDefaultResponseType]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterMerchantReq request)
        {
            await _mediator.Send(new RegisterMerchantCommand(request.MerchantName, request.FirstName, request.LastName, request.Phone, request.Email,
                request.Password));

            return Created("Register", "");
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginReq req)
        {
            var response = await _authenticationService.Authenticate(req.EmailOrPhone, req.Password, req.ExpiryInDay);
            return Ok(response);
        }

        [HttpGet("check")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Check()
        {
            var claims = User.Claims;

            var token = Request.Headers["Authorization"];
            var bearer = token.ToString().Split(' ')[1];
            var response = await _authenticationService.Refresh(bearer, claims);
            return Ok(response);
        }

        [HttpPost("forgotPassword")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordReq req)
        {
            await _mediator.Send(new ForgotPasswordCommand(req.Phone));
            return Accepted();
        }

        [HttpPost("verify/{phone}")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Verify([FromBody] VerifyReq req, [FromRoute] string phone)
        {
            if (req.IsRegistered)
                await _mediator.Send(new ForgotPasswordCommand(phone));
            return Accepted();
        }

        [HttpPost("verify/{phone}/check")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Verify([FromRoute] string phone)
        {
            //await _mediator.Send(new ForgotPasswordCommand(phone));
            return Accepted();
        }
    }
}
