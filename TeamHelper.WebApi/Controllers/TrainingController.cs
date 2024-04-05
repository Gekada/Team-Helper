using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TeamHelper.Application.Trainings.Commands;
using TeamHelper.Application.Trainings.Queries;

namespace TeamHelper.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class TrainingController: BaseController
    {
        [HttpGet]
        public async Task<ActionResult<TrainingListVM>> GetAll()
        {
            var vm = await Mediator.Send(new GetTrainingsQuery());
            return Ok(vm);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TrainingVM>> Get(Guid id)
        {
            var query = new GetTrainingQuery()
            {
                Id = id
            };
            var vm = await Mediator.Send(query);
            return Ok(vm);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TrainingVM>> GetCoachTrainings(Guid id)
        {
            var query = new GetCoachesTrainingsQuery()
            {
                Id = id
            };
            var vm = await Mediator.Send(query);
            return Ok(vm);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TrainingVM>> GetAthletesTrainings(Guid id)
        {
            var query = new GetAthleteTrainingsQuery()
            {
                Id = id
            };
            var vm = await Mediator.Send(query);
            return Ok(vm);
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> Create([FromBody] CreateTrainingCommand command)
        {
            var vm = await Mediator.Send(command);
            return Ok(vm);
        }

        [HttpPost("{id}")]
        public async Task<ActionResult> StopTheTraining(Guid id)
        {
            var command = new StopTrainingCommand()
            {
                Id = id
            };
            var vm = await Mediator.Send(command);
            return Ok();
        }

        [HttpPost("{id}")]
        public async Task<ActionResult> StartTheTraining(Guid id)
        {
            var command = new StartTrainingCommand()
            {
                Id = id
            };
            var vm = await Mediator.Send(command);
            return Ok();
        }


        [HttpPut]
        public async Task<ActionResult> Update([FromBody] UpdateTrainingCommand command)
        {
            var vm = await Mediator.Send(command);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var command = new DeleteTrainingCommand()
            {
                Id = id
            };
            var vm = await Mediator.Send(command);
            return Ok();
        }
    }
}
