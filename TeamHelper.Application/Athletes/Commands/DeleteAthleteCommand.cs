using TeamHelper.Application.Interfaces;
using TeamHelper.Domain;
using MediatR;
using TeamHelper.Application.Common.Exceptions;

namespace TeamHelper.Application.Athletes.Commands
{
    public class DeleteAthleteCommand : IRequest
    {
        public Guid Id { get; set; }
    }
    public class DeleteAthleteCommandHandler : IRequestHandler<DeleteAthleteCommand>
    {
        private readonly ITeamHelperDBContext context;
        public DeleteAthleteCommandHandler(ITeamHelperDBContext _context) => context = _context;
        public async Task<Unit> Handle(DeleteAthleteCommand request, CancellationToken cancellationToken)
        {
            var entity = await context.Athletes.FindAsync(new object[] { request.Id }, cancellationToken);
            if (entity == null || entity.Id != request.Id)
            {
                throw new NotFoundException(nameof(Athlete), request.Id);
            }
            context.Athletes.Remove(entity);
            await context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}