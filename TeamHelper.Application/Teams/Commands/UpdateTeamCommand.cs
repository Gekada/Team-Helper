using TeamHelper.Application.Interfaces;
using TeamHelper.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TeamHelper.Application.Common.Exceptions;

namespace TeamHelper.Application.Teams.Commands
{
    public class UpdateTeamCommand : IRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid CoachId { get; set; }
    }
    public class UpdateTeamCommandHandler : IRequestHandler<UpdateTeamCommand>
    {
        private readonly ITeamHelperDBContext context;
        public UpdateTeamCommandHandler(ITeamHelperDBContext _context) => context = _context;
        public async Task<Unit> Handle(UpdateTeamCommand request, CancellationToken cancellationToken)
        {
            var entity = await context.Teams.FirstOrDefaultAsync(node => node.Id == request.Id, cancellationToken);
            if (entity == null || entity.Id != request.Id)
            {
                throw new NotFoundException(nameof(Team), request.Id);
            }
            var coach = await context.Coaches.FirstOrDefaultAsync(node => node.Id == request.CoachId, cancellationToken);
            if (coach == null || coach.Id != request.CoachId)
            {
                throw new NotFoundException(nameof(Coach), request.CoachId);
            }
            entity.Name = request.Name;
            entity.Coach = coach;
            await context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
