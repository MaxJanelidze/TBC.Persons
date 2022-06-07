using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.API.ActionFilters;
using System.Collections.Generic;
using System.Threading.Tasks;
using TBC.Persons.Application.Queries.Cities.GetCities;

namespace TBC.Persons.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CitiesController : ControllerBase
    {
        private readonly ISender _mediator;
        private readonly IMapper _mapper;

        public CitiesController(ISender mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet]
        [PaginationHeader]
        [ProducesResponseType(typeof(IEnumerable<CitiesListItemModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCities([FromQuery] GetCitiesModel request)
        {
            var query = _mapper.Map<GetCitiesModel, GetCitiesQuery>(request);

            var result = await _mediator.Send(query);

            return Ok(result);
        }
    }
}
