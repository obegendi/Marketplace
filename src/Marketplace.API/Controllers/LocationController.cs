using System.Threading.Tasks;
using Marketplace.Application.LocationServices.GetCities;
using Marketplace.Application.LocationServices.GetCountries;
using Marketplace.Application.LocationServices.GetDistricts;
using Marketplace.Application.LocationServices.GetTowns;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Marketplace.API.Controllers
{
    [Route("v1/location")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LocationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("countries")]
        public async Task<IActionResult> Countries()
        {
            var response = await _mediator.Send(new GetCountryQuery());

            return Ok(response);
        }

        [HttpGet("{country}/cities")]
        public async Task<IActionResult> Cities([FromRoute] string country)
        {
            var response = await _mediator.Send(new GetCitiesQuery(country));

            return Ok(response);
        }

        [HttpGet("{country}/{city}/towns")]
        public async Task<IActionResult> Towns([FromRoute] string country, [FromRoute] string city)
        {
            var response = await _mediator.Send(new GetTownQuery(city));

            return Ok(response);
        }

        [HttpGet("{country}/{city}/{town}/districts")]
        public async Task<IActionResult> Districts([FromRoute] string country, [FromRoute] string city, [FromRoute] string town)
        {
            var response = await _mediator.Send(new GetDistrictQuery(town, city));

            return Ok(response);
        }
    }
}
