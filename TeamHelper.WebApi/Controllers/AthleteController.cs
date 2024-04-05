using AutoMapper;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using TeamHelper.Application.Athletes.Commands;
using TeamHelper.Application.Athletes.Queries;
using TeamHelper.Domain;
using TeamHelper.WebApi.Models;

namespace TeamHelper.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AthleteController : BaseController
    {
        private readonly IMapper mapper;
        public AthleteController(IMapper _mapper) => mapper = _mapper;

        [HttpGet]
        public async Task<ActionResult<AthleteListVM>> GetAll()
        {
            var vm = await Mediator.Send(new GetAthletesQuery());
            return Ok(vm);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AthleteVM>> Get(Guid id)
        {
            var query = new GetAthleteQuery()
            {
                Id = id
            };
            var vm = await Mediator.Send(query);
            return Ok(vm);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<Training>>> GetWeeklyPerfomance(Guid id)
        {
            var query = new GetWeeklyPerfomanceQuery()
            {
                AthleteId = id
            };
            var vm = await Mediator.Send(query);
            return Ok(vm);
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> Create([FromBody] CreateAthleteCommandDTO createDTO)
        {
            base.InitClaims();
            var command = mapper.Map<CreateAthleteCommand>(createDTO);
            command.OrganizationId = UserId;
            var vm = await Mediator.Send(command);
            return Ok(vm);
        }

        [HttpPut]
        public async Task<ActionResult> Update([FromBody] UpdateAthleteCommand updateDTO)
        {
            var vm = await Mediator.Send(updateDTO);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var command = new DeleteAthleteCommand()
            {
                Id = id
            };
            var vm = await Mediator.Send(command);
            return Ok();
        }
    }
}
