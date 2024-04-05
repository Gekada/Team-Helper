using TeamHelper.Application.Interfaces;
using TeamHelper.Domain;
using MediatR;
using TeamHelper.Application.Common.Exceptions;
namespace TeamHelper.Application.Organizations.Commands
{
    public class DeleteOrganizationCommand : IRequest
    {
        public Guid Id { get; set; }
    }
    public class DeleteOrganizationCommandHandler : IRequestHandler<DeleteOrganizationCommand>
    {
        private readonly ITeamHelperDBContext context;
        public DeleteOrganizationCommandHandler(ITeamHelperDBContext _context) => context = _context;
        public async Task<Unit> Handle(DeleteOrganizationCommand request, CancellationToken cancellationToken)
        {
            var entity = await context.Organizations.FindAsync(new object[] {request.Id},cancellationToken);
            if (entity == null || entity.Id != request.Id)
            {
                throw new NotFoundException(nameof(Organization), request.Id);
            }
            context.Organizations.Remove(entity);
            await context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}