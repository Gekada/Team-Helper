using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TeamHelper.Application.Coaches.Commands;
using TeamHelper.Application.Coaches.Queries;
using TeamHelper.WebApi.Models;

namespace TeamHelper.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class CoachController : BaseController
    {
        private readonly IMapper mapper;
        public CoachController(IMapper _mapper) => mapper = _mapper;
        
        [HttpGet]
        public async Task<ActionResult<CoachListVM>> GetAll()
        {
            var vm = await Mediator.Send(new GetCoachesQuery());
            return Ok(vm);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CoachVM>> Get(Guid id)
        {
            var query = new GetCoachQuery
            {
                Id = id
            };
            var vm = await Mediator.Send(query);
            return Ok(vm);
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> Create([FromBody] CreateCoachCommandDTO createDTO)
        {
            InitClaims();
            var command = mapper.Map<CreateCoachCommand>(createDTO);
            command.OrganizationId = UserId;
            var vm = await Mediator.Send(command);
            return Ok(vm);
        }

        [HttpPut]
        public async Task<ActionResult> Update([FromBody] UpdateCoachCommandDTO updateDTO)
        {
            InitClaims();
            var command = mapper.Map<UpdateCoachCommand>(updateDTO);
            command.Id = UserId;
            var vm = await Mediator.Send(command);
            return Ok();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var command = new DeleteCoachCommand()
            {
                Id = id
            };
            var vm = await Mediator.Send(command);
            return Ok();
        }
    }
}
