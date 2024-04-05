using TeamHelper.Application.Interfaces;
using TeamHelper.Domain;
using MediatR;
namespace TeamHelper.Application.Organizations.Commands
{
    public class CreateOrganizationCommand:IRequest<Guid>
    {
        public string Name { get; set; }
        public string Adress { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
    }
    public class CreateOrganizationCommandHandler : IRequestHandler<CreateOrganizationCommand, Guid>
    {
        private readonly ITeamHelperDBContext context;
        public CreateOrganizationCommandHandler(ITeamHelperDBContext _context) => context = _context;
        public async Task<Guid> Handle(CreateOrganizationCommand request, CancellationToken cancellationToken)
        {
            var organization = new Organization
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Adress = request.Adress,
                PhoneNumber = request.PhoneNumber,
                Email = request.Email
            };
            await context.Organizations.AddAsync(organization, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);
            return organization.Id;
        }
    }
}
