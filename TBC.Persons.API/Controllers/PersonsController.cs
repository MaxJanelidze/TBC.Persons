using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Shared.API.ActionFilters;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TBC.Persons.Application.Commands.Persons.AddRelatedPerson;
using TBC.Persons.Application.Commands.Persons.Create;
using TBC.Persons.Application.Commands.Persons.Delete;
using TBC.Persons.Application.Commands.Persons.RemoveRelatedPerson;
using TBC.Persons.Application.Commands.Persons.Update;
using TBC.Persons.Application.Commands.Persons.UploadPicture;
using TBC.Persons.Application.Queries.Persons.GetPerson;
using TBC.Persons.Application.Queries.Persons.GetPersons;
using TBC.Persons.Application.Queries.Persons.GetRelationshipReport;

namespace TBC.Persons.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonsController : ControllerBase
    {
        private readonly ISender _mediator;
        private readonly IMapper _mapper;

        public PersonsController(ISender mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet]
        [PaginationHeader]
        [ProducesResponseType(typeof(IEnumerable<PersonsListItemModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetPersons([FromQuery] GetPersonsModel request)
        {
            var query = _mapper.Map<GetPersonsModel, GetPersonsQuery>(request);

            var result = await _mediator.Send(query);

            return Ok(result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(PersonModel), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetPerson([FromRoute] int id)
        {
            var query = new GetPersonQuery { Id = id };
            var person = await _mediator.Send(query);

            return Ok(person);
        }

        [HttpPost]
        [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
        public async Task<IActionResult> CreatePerson([FromBody] CreatePersonModel request)
        {
            var command = _mapper.Map<CreatePersonModel, CreatePersonCommand>(request);

            var personId = await _mediator.Send(command);

            return Created(string.Empty, personId);
        }

        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> ChangePersonDetails([FromRoute] int id, [FromBody] ChangePersonDetailsModel request)
        {
            var command = _mapper.Map<ChangePersonDetailsModel, ChangePersonDetailsCommand>(request);
            command.Id = id;

            await _mediator.Send(command);

            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeletePerson([FromRoute] int id)
        {
            var command = new DeletePersonCommand { Id = id };

            await _mediator.Send(command);

            return NoContent();
        }

        [HttpPost("related-person")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> AddRelatedPerson([FromBody] AddRelatedPersonModel request)
        {
            var command = new AddRelatedPersonCommand
            {
                MastertPersonId = request.MastertPersonId,
                RelatedPersonId = request.RelatedPersonId,
                RelationType = request.RelationType
            };

            await _mediator.Send(command);

            return NoContent();
        }

        [HttpDelete("related-person")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> RemoveRelatedPerson([FromBody] RemoveRelatedPersonModel request)
        {
            var command = new RemoveRelatedPersonCommand
            {
                MastertPersonId = request.MastertPersonId,
                RelatedPersonId = request.RelatedPersonId
            };

            await _mediator.Send(command);

            return NoContent();
        }

        [HttpGet("relationship-report")]
        [ProducesResponseType(typeof(IEnumerable<RelationshipReportItem>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetRelationshipReport()
        {
            var result = await _mediator.Send(new GetRelationshipReportQuery());

            return Ok(result);
        }

        [HttpPost("{personId}/upload-picture")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        public async Task<IActionResult> UploadPicture([FromRoute] int personId, IFormFile file)
        {
            var pictureAddress = await _mediator.Send(new UploadPictureCommand
            {
                PersonId = personId,
                Picture = file
            });

            return Ok(pictureAddress);
        }
    }
}
