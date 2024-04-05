using TeamHelper.Application.Interfaces;
using TeamHelper.Domain;
using MediatR;
using TeamHelper.Application.Common.Exceptions;
namespace TeamHelper.Application.Coaches.Commands
{
    public class DeleteCoachCommand : IRequest
    {
        public Guid Id { get; set; }
    }
    public class DeleteCoachCommandHandler : IRequestHandler<DeleteCoachCommand>
    {
        private readonly ITeamHelperDBContext context;
        public DeleteCoachCommandHandler(ITeamHelperDBContext _context) => context = _context;
        public async Task<Unit> Handle(DeleteCoachCommand request, CancellationToken cancellationToken)
        {
            var entity = await context.Coaches.FindAsync(new object[] { request.Id }, cancellationToken);
            if (entity == null || entity.Id != request.Id)
            {
                throw new NotFoundException(nameof(Coach), request.Id);
            }
            context.Coaches.Remove(entity);
            await context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}