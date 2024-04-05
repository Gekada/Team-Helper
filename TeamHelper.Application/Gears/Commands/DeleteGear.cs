using TeamHelper.Application.Interfaces;
using TeamHelper.Domain;
using MediatR;
using TeamHelper.Application.Common.Exceptions;

namespace TeamHelper.Application.Gears.Commands
{
    public class DeleteGearCommand : IRequest
    {
        public Guid Id { get; set; }
    }
    public class DeleteGearCommandHandler : IRequestHandler<DeleteGearCommand>
    {
        private readonly ITeamHelperDBContext context;
        public DeleteGearCommandHandler(ITeamHelperDBContext _context) => context = _context;
        public async Task<Unit> Handle(DeleteGearCommand request, CancellationToken cancellationToken)
        {
            var entity = await context.Gears.FindAsync(new object[] { request.Id }, cancellationToken);
            if (entity == null || entity.Id != request.Id)
            {
                throw new NotFoundException(nameof(Gear), request.Id);
            }
            context.Gears.Remove(entity);
            await context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}