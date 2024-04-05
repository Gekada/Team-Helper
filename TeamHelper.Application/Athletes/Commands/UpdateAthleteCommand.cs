using TeamHelper.Application.Interfaces;
using TeamHelper.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TeamHelper.Application.Common.Exceptions;

namespace TeamHelper.Application.Athletes.Commands
{
    public class UpdateAthleteCommand : IRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public Guid OrganizationId { get; set; }
    }
    public class UpdateAthleteCommandHandler : IRequestHandler<UpdateAthleteCommand>
    {
        private readonly ITeamHelperDBContext context;
        public UpdateAthleteCommandHandler(ITeamHelperDBContext _context) => context = _context;
        public async Task<Unit> Handle(UpdateAthleteCommand request, CancellationToken cancellationToken)
        {
            var entity = await context.Athletes.FirstOrDefaultAsync(node => node.Id == request.Id, cancellationToken);
            if (entity == null || entity.Id != request.Id)
            {
                throw new NotFoundException(nameof(Athlete), request.Id);
            }
            var organization = await context.Organizations.FirstOrDefaultAsync(node => node.Id == request.OrganizationId, cancellationToken);
            if (organization == null || organization.Id != request.OrganizationId)
            {
                throw new NotFoundException(nameof(Organization), request.OrganizationId);
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
