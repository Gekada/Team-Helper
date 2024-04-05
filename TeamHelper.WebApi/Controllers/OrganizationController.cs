using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TeamHelper.Application.Organizations.Commands;
using TeamHelper.Application.Organizations.Queries;
using TeamHelper.WebApi.Models;

namespace TeamHelper.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class OrganizationController: BaseController
    {
        private readonly IMapper mapper;
        public OrganizationController(IMapper _mapper) => mapper = _mapper;
        [HttpGet]
        public async Task<ActionResult<OrganizationListVM>> GetAll()
        {
            var vm = await Mediator.Send(new GetOrganizationsQuery());
            return Ok(vm);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrganizationVM>> Get(Guid id)
        {
            var query = new GetOrganizationQuery
            {
                Id = id
            };
            var vm = await Mediator.Send(query);
            return Ok(vm);
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> Create([FromBody] CreateOrganizationCommand command)
        {
            var vm = await Mediator.Send(command);
            return Ok(vm);
        }

        [HttpPut]
        public async Task<ActionResult> Update([FromBody] UpdateOrganizationCommandDTO updateDTO)
        {
            base.InitClaims();
            var command = mapper.Map<UpdateOrganizationCommand>(updateDTO);
            command.Id = UserId;
            var vm = await Mediator.Send(command);
            return Ok();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var command = new DeleteOrganizationCommand()
            {
                Id = id
            };
            var vm = await Mediator.Send(command);
            return Ok();
        }
    }
}
