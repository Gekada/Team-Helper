using TeamHelper.Application.Interfaces;
using TeamHelper.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TeamHelper.Application.Common.Exceptions;

namespace TeamHelper.Application.Teams.Commands
{
    public class CreateTeamCommand : IRequest<Guid>
    {
        public string Name { get; set; }
        public int MembNumber { get; set; }
        public Guid CoachId { get; set; }
    }
    public class CreateTeamCommandHandler : IRequestHandler<CreateTeamCommand, Guid>
    {
        private readonly ITeamHelperDBContext context;
        public CreateTeamCommandHandler(ITeamHelperDBContext _context) => context = _context;
        public async Task<Guid> Handle(CreateTeamCommand request, CancellationToken cancellationToken)
        {
            var coach = await context.Coaches.FirstOrDefaultAsync(node => node.Id == request.CoachId, cancellationToken);
            if (coach == null || coach.Id != request.CoachId)
            {
                throw new NotFoundException(nameof(Coach), request.CoachId);
            }
            var entity = new Team
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                MembNumber = 0,
                Coach = coach,
                Athlete = {}
            };
            await context.Teams.AddAsync(entity, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);
            return entity.Id;
        }
    }
}
