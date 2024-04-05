using TeamHelper.Application.Interfaces;
using TeamHelper.Domain;
using MediatR;
using TeamHelper.Application.Common.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace TeamHelper.Application.Trainings.Commands
{
    public class StopTrainingCommand : IRequest
    {
        public Guid Id { get; set; }
    }
    public class StopTrainingCommandHandler : IRequestHandler<StopTrainingCommand>
    {
        private readonly ITeamHelperDBContext context;
        public StopTrainingCommandHandler(ITeamHelperDBContext _context) => context = _context;
        public async Task<Unit> Handle(StopTrainingCommand request, CancellationToken cancellationToken)
        {
            var entity = await context.Trainings.FirstOrDefaultAsync(node => node.Id == request.Id, cancellationToken);
            if (entity == null || entity.Id != request.Id)
            {
                throw new NotFoundException(nameof(Training), request.Id);
            }
            using (HttpClient httpClient = new HttpClient())
            {
                // The URL of the API endpoint you want to send the POST request to
                string apiUrl = "http://192.168.50.247:80/stopIndicators";

                // Convert the data to JSON format

                var text = "";

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
            entity.IsInprocess = false;
            await context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}