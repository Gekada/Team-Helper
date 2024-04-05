using Microsoft.AspNetCore.Mvc;
using TeamHelper.Application.Athletes.Queries;
using TeamHelper.Application.Gears.Commands;
using TeamHelper.Application.Gears.Queries;

namespace TeamHelper.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class GearController: BaseController
    {
        [HttpGet]
        public async Task<ActionResult<GearListVM>> GetAll()
        {
            var vm = await Mediator.Send(new GetGearsQuery());
            return Ok(vm);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GearVM>> Get(Guid id)
        {
            var query = new GetGearQuery()
            {
                Id = id
            };
            var vm = await Mediator.Send(query);
            return Ok(vm);
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> Create([FromBody] CreateGearCommand command)
        {
            var vm = await Mediator.Send(command);
            return Ok(vm);
        }

        [HttpPut]
        public async Task<ActionResult> Update([FromBody] UpdateGearCommand command)
        {
            var vm = await Mediator.Send(command);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var command = new DeleteGearCommand()
            {
                Id = id
            };
            var vm = await Mediator.Send(command);
            return Ok();
        }
    }
}
