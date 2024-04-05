using TeamHelper.Application.Interfaces;
using TeamHelper.Domain;
using MediatR;
using TeamHelper.Application.Common.Exceptions;
using Microsoft.EntityFrameworkCore;
using TeamHelper.Application.Trainings.Queries;

namespace TeamHelper.Application.Trainings.Commands
{
    public class StartTrainingCommand : IRequest
    {
        public Guid Id { get; set; }
    }
    public class StartTrainingCommandHandler : IRequestHandler<StartTrainingCommand>
    {
        private readonly ITeamHelperDBContext context;
        public StartTrainingCommandHandler(ITeamHelperDBContext _context) => context = _context;
        public async Task<Unit> Handle(StartTrainingCommand request, CancellationToken cancellationToken)
        {
            context.Athletes.Load();
            context.Teams.Load();
            context.Gears.Load();
            context.AthleteIndicators.Load();
            var entity = await context.Trainings.FirstOrDefaultAsync(node => node.Id == request.Id, cancellationToken);
            if (entity == null || entity.Id != request.Id)
            {
                throw new NotFoundException(nameof(Training), request.Id);
            }
            entity.IsInprocess = true;
            Domain.AthleteIndicators indicator = null;
            foreach (var athlete in entity.Team.Athlete)
            {
                var indicators = new Domain.AthleteIndicators
                {
                    Training = entity,
                    Athlete = athlete,
                    Gear = context.Gears.First<Gear>(),
                    GearId = context.Gears.First<Gear>().Id
                };
                indicator = indicators;
                await context.AthleteIndicators.AddAsync(indicators);
            }
            foreach(var ind in context.AthleteIndicators)
            {
                ind.GearId = Guid.Empty;
            }
            using (HttpClient httpClient = new HttpClient())
            {
                // The URL of the API endpoint you want to send the POST request to
                string apiUrl = "http://192.168.50.247:80/setIndicators";

                // Convert the data to JSON format

                var text = $"\"indicatorsId\": \"{indicator.Id}\"";

                text = '{' + text + '}';
                    
                var jsonContent = new StringContent(text, System.Text.Encoding.UTF8, "text/plain");


                // Send the POST request
                HttpResponseMessage response = await httpClient.PostAsync(apiUrl, jsonContent);

                // Check the response status
                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    Console.WriteLine("Request successful. Response:");
                    Console.WriteLine(responseBody);
                }
                else
                {
                    Console.WriteLine("Request failed. Status code: " + response.StatusCode);
                }
            }
            await context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}