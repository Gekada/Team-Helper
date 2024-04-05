using TeamHelper.Application.Interfaces;
using TeamHelper.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TeamHelper.Application.Common.Exceptions;

namespace TeamHelper.Application.Athletes.Commands
{
    public class CreateAthleteCommand : IRequest<Guid>
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public Guid OrganizationId { get; set; }
    }
    public class CreateAthleteCommandHandler : IRequestHandler<CreateAthleteCommand, Guid>
    {
        private readonly ITeamHelperDBContext context;
        public CreateAthleteCommandHandler(ITeamHelperDBContext _context) => context = _context;
        public async Task<Guid> Handle(CreateAthleteCommand request, CancellationToken cancellationToken)
        {
            var organization = await context.Organizations.FirstOrDefaultAsync(node => node.Id == request.OrganizationId, cancellationToken);
            if (organization == null || organization.Id != request.OrganizationId)
            {
                throw new NotFoundException(nameof(Organization), request.OrganizationId);
            }
            var entity = new Athlete
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Age = request.Age,
                PhoneNumber = request.PhoneNumber,
                Email = request.Email,
                Organization = organization
            };
            await context.Athletes.AddAsync(entity, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);
            return entity.Id;
        }
    }
}
