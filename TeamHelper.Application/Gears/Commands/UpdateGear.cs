using TeamHelper.Application.Interfaces;
using TeamHelper.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TeamHelper.Application.Common.Exceptions;

namespace TeamHelper.Application.Gears.Commands
{
    public class UpdateGearCommand : IRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
    public class UpdateGearCommandHandler : IRequestHandler<UpdateGearCommand>
    {
        private readonly ITeamHelperDBContext context;
        public UpdateGearCommandHandler(ITeamHelperDBContext _context) => context = _context;
        public async Task<Unit> Handle(UpdateGearCommand request, CancellationToken cancellationToken)
        {
            var entity = await context.Gears.FirstOrDefaultAsync(node => node.Id == request.Id, cancellationToken);
            if (entity == null || entity.Id != request.Id)
            {
                throw new NotFoundException(nameof(Gear), request.Id);
            }
            entity.Name = request.Name;
            await context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
