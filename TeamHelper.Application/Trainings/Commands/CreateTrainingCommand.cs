using TeamHelper.Application.Interfaces;
using TeamHelper.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TeamHelper.Application.Common.Exceptions;

namespace TeamHelper.Application.Trainings.Commands
{
    public class CreateTrainingCommand : IRequest<Guid>
    {
        public DateTime Date { get; set; }
        public string Location { get; set; }
        public string Duration { get; set; }
        public Guid TeamId { get; set; }
    }
    public class CreateTrainingCommandHandler : IRequestHandler<CreateTrainingCommand, Guid>
    {
        private readonly ITeamHelperDBContext context;
        public CreateTrainingCommandHandler(ITeamHelperDBContext _context) => context = _context;
        public async Task<Guid> Handle(CreateTrainingCommand request, CancellationToken cancellationToken)
        {
            var team = await context.Teams.FirstOrDefaultAsync(node => node.Id == request.TeamId, cancellationToken);
            if (team == null || team.Id != request.TeamId)
            {
                throw new NotFoundException(nameof(Team), request.TeamId);
            }
            var entity = new Training
            {
                Id = Guid.NewGuid(),
                Location = request.Location,
                Duration = request.Duration,
                Date = request.Date,
                Team = team,
                IsInprocess = false
            };
            await context.Trainings.AddAsync(entity, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);
            return entity.Id;
        }
    }
}
