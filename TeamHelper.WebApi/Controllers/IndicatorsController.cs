using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TeamHelper.Application.AthleteIndicators.Commands;
using TeamHelper.Application.AthleteIndicators.Queries;

namespace TeamHelper.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class IndicatorsController: BaseController
    {
        [HttpGet]
        public async Task<ActionResult<IndicatorsListVM>> GetAll()
        {
            var vm = await Mediator.Send(new GetIndicatorsListQuery());
            return Ok(vm);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IndicatorsVM>> Get(Guid id)
        {
            var query = new GetIndicatorsQuery()
            {
                Id = id
            };
            var vm = await Mediator.Send(query);
            return Ok(vm);
        }

        [HttpPost]
        public async Task<ActionResult<AthleteIndicatorsDataListVM>> GetIndicatorsForAthlete([FromBody] GetIndicatorsForAthleteListQuery query)
        {
            var vm = await Mediator.Send(query);
            return Ok(vm);
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> Create([FromBody] CreateIndicatorsCommand command)
        {
            var vm = await Mediator.Send(command);
            return Ok(vm);
        }
        //maybe delete
        [HttpPut]
        public async Task<ActionResult> Update([FromBody] UpdateIndicatorsCommand command)
        {
            var vm = await Mediator.Send(command);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var command = new DeleteIndicatorsCommand()
            {
                Id = id
            };
            var vm = await Mediator.Send(command);
            return Ok();
        }
    }
}
