using Microsoft.AspNetCore.Mvc;
using TeamHelper.Application.IndicatorsData.Commands;
using TeamHelper.Application.IndicatorsData.Queries;

namespace TeamHelper.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class IndicatorsDataController: BaseController
    {
        [HttpGet]
        public async Task<ActionResult<IndicatorsDataListVM>> GetAll()
        {
            var vm = await Mediator.Send(new GetIndicatorsDataListQuery());
            return Ok(vm);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IndicatorsDataVM>> Get(Guid id)
        {
            var query = new GetIndicatorsDataQuery()
            {
                Id = id
            };
            var vm = await Mediator.Send(query);
            return Ok(vm);
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> Create([FromBody] CreateIndicatorsDataCommand command)
        {
            var vm = await Mediator.Send(command);
            return Ok(vm);
        }

        //maybe delete
        [HttpPut]
        public async Task<ActionResult> Update([FromBody] UpdateIndicatorsDataCommand command)
        {
            var vm = await Mediator.Send(command);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var command = new DeleteIndicatorsDataCommand()
            {
                Id = id
            };
            var vm = await Mediator.Send(command);
            return Ok();
        }
    }
}
