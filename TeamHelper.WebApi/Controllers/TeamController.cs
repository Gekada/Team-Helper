using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TeamHelper.Application.Athletes.Commands;
using TeamHelper.Application.Teams.Commands;
using TeamHelper.Application.Teams.Queries;
using TeamHelper.Domain;
using TeamHelper.WebApi.Models;

namespace TeamHelper.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class TeamController: BaseController
    {

        [HttpGet]
        public async Task<ActionResult<TeamListVM>> GetAll()
        {
            var vm = await Mediator.Send(new GetTeamsQuery());
            return Ok(vm);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TeamVM>> GetAthletesTeam(Guid id)
        {
            var vm = await Mediator.Send(new GetAthletesTeamQuery() { Id = id});
            return Ok(vm);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CoachesTeamListVM>> GetCoachesTeams(Guid id)
        {
            var vm = await Mediator.Send(new GetCoachesTeamsQuery() { CoachId = id });
            return Ok(vm);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ICollection<Athlete>>> GetAthletesByTeam(Guid id)
        {
            var vm = await Mediator.Send(new GetAthletesByTeamQuery() { Id = id });
            return Ok(vm);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TeamVM>> Get(Guid id)
        {
            var query = new GetTeamQuery()
            {
                Id = id
            };
            var vm = await Mediator.Send(query);
            return Ok(vm);
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> Create([FromBody] CreateTeamCommand command)
        {
            var vm = await Mediator.Send(command);
            return Ok(vm);
        }

        [HttpPut]
        public async Task<ActionResult> Update([FromBody] UpdateTeamCommand command)
        {
            var vm = await Mediator.Send(command);
            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult> AddAthlete([FromBody] AddTeamAthleteCommand command)
        {
            var vm = await Mediator.Send(command);
            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult> RemoveAthlete([FromBody] RemoveTeamAthleteCommand command)
        {
            var vm = await Mediator.Send(command);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var command = new DeleteTeamCommand()
            {
                Id = id
            };
            var vm = await Mediator.Send(command);
            return Ok();
        }

    }
}
