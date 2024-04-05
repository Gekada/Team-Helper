using TeamHelper.Application.Interfaces;
using TeamHelper.Domain;
using MediatR;

namespace TeamHelper.Application.Gears.Commands
{
    public class CreateGearCommand : IRequest<Guid>
    {
        public string Name { get; set; }
    }
    public class CreateGearCommandHandler : IRequestHandler<CreateGearCommand, Guid>
    {
        private readonly ITeamHelperDBContext context;
        public CreateGearCommandHandler(ITeamHelperDBContext _context) => context = _context;
        public async Task<Guid> Handle(CreateGearCommand request, CancellationToken cancellationToken)
        {
            if (context.Gears.FirstOrDefault(node => node.Name == request.Name) != null)
            {
                return context.Gears.FirstOrDefault(node => node.Name == request.Name).Id;
            }
            var entity = new Gear
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
            };
            await context.Gears.AddAsync(entity, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);
            return entity.Id;
        }
    }
}
