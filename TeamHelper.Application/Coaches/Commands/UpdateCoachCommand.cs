using TeamHelper.Application.Interfaces;
using TeamHelper.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TeamHelper.Application.Common.Exceptions;

namespace TeamHelper.Application.Coaches.Commands
{
    public class UpdateCoachCommand : IRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public Guid OrganizationId { get; set; }
    }
    public class UpdateCoachCommandHandler : IRequestHandler<UpdateCoachCommand>
    {
        private readonly ITeamHelperDBContext context;
        public UpdateCoachCommandHandler(ITeamHelperDBContext _context) => context = _context;
        public async Task<Unit> Handle(UpdateCoachCommand request, CancellationToken cancellationToken)
        {
            var entity = await context.Coaches.FirstOrDefaultAsync(node => node.Id == request.Id,cancellationToken);
            if (entity == null || entity.Id != request.Id)
            {
                throw new NotFoundException(nameof(Coach), request.Id);
            }
            var organization = await context.Organizations.FirstOrDefaultAsync(node => node.Id == request.OrganizationId,cancellationToken);
            if (organization == null || organization.Id != request.OrganizationId)
            {
                throw new NotFoundException(nameof(Organization), request.Id);
            }
            entity.Name = request.Name;
            entity.Age = request.Age;   
            entity.PhoneNumber = request.PhoneNumber;
            entity.Email = request.Email;
            entity.Organization = organization;
            await context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
