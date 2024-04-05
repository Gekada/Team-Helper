using TeamHelper.Application.Interfaces;
using TeamHelper.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TeamHelper.Application.Common.Exceptions;
namespace TeamHelper.Application.Organizations.Commands
{
    public class UpdateOrganizationCommand:IRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Adress { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
    }
    public class UpdateOrganizationCommandHandler : IRequestHandler<UpdateOrganizationCommand>
    {
        private readonly ITeamHelperDBContext context;
        public UpdateOrganizationCommandHandler(ITeamHelperDBContext _context) => context = _context;
        public async Task<Unit> Handle(UpdateOrganizationCommand request, CancellationToken cancellationToken)
        {
            var organization = await context.Organizations.FirstOrDefaultAsync(organization => organization.Id == request.Id, cancellationToken);
            if (organization == null || organization.Id != request.Id)
            {
                throw new NotFoundException(nameof(Organization), request.Id);
            }
            organization.Name = request.Name;
            organization.PhoneNumber = request.PhoneNumber;
            organization.Adress = request.Adress;
            organization.Email = request.Email;
            await context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }

    }
}
