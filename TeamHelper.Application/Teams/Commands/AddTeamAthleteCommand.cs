using TeamHelper.Application.Interfaces;
using TeamHelper.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TeamHelper.Application.Common.Exceptions;

namespace TeamHelper.Application.Teams.Commands
{
    public class AddTeamAthleteCommand : IRequest
    {
        public Guid TeamId { get; set; }
        public Guid AthleteId { get; set; }
    }
    public class AddTeamAthleteCommandHandler : IRequestHandler<AddTeamAthleteCommand>
    {
        private readonly ITeamHelperDBContext context;
        public AddTeamAthleteCommandHandler(ITeamHelperDBContext _context) => context = _context;
        public async Task<Unit> Handle(AddTeamAthleteCommand request, CancellationToken cancellationToken)
        {
            var entity = await context.Teams.FirstOrDefaultAsync(node => node.Id == request.TeamId, cancellationToken);
            if (entity == null || entity.Id != request.TeamId)
            {
                throw new NotFoundException(nameof(Team), request.TeamId);
            }
            var athlete = await context.Athletes.FirstOrDefaultAsync(node => node.Id == request.AthleteId, cancellationToken);
            if (athlete == null || athlete.Id != request.AthleteId)
            {
                throw new NotFoundException(nameof(Athlete), request.AthleteId);
            }
            entity.MembNumber++;
            Console.WriteLine($"Added team member: {entity.MembNumber}");
            entity.Athlete.Add(athlete);
            await context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
