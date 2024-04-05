using TeamHelper.Application.Interfaces;
using TeamHelper.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TeamHelper.Application.Common.Exceptions;

namespace TeamHelper.Application.Teams.Commands
{
    public class RemoveTeamAthleteCommand : IRequest
    {
        public Guid TeamId { get; set; }
        public Guid AthleteId { get; set; }
    }
    public class RemoveTeamAthleteCommandHandler : IRequestHandler<RemoveTeamAthleteCommand>
    {
        private readonly ITeamHelperDBContext context;
        public RemoveTeamAthleteCommandHandler(ITeamHelperDBContext _context) => context = _context;
        public async Task<Unit> Handle(RemoveTeamAthleteCommand request, CancellationToken cancellationToken)
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
            if (entity.Athlete.Contains(athlete))
            {
                entity.MembNumber--;
                entity.Athlete.Remove(athlete);
                await context.SaveChangesAsync(cancellationToken);
            }
            return Unit.Value;
        }
    }
}
