using TeamHelper.Application.Interfaces;
using TeamHelper.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TeamHelper.Application.Common.Exceptions;
using TeamHelper.Application.Common.Mapping;
using AutoMapper;

namespace TeamHelper.Application.Organizations.Queries
{
    public class OrganizationVM:IMapWith<Organization>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Adress { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public ICollection<Coach>? Coaches { get; set; }
        public ICollection<Athlete>? Athletes { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Organization, OrganizationVM>();
        }
    }
    public class GetOrganizationQuery: IRequest<OrganizationVM>
    {
        public Guid Id { get; set; }
    }
    public class GetOrganizationQueryHandler: IRequestHandler<GetOrganizationQuery,OrganizationVM>
    {
        private readonly ITeamHelperDBContext context;
        private readonly IMapper mapper;
        public GetOrganizationQueryHandler(ITeamHelperDBContext _context, IMapper _mapper)
        {
            context = _context;
            mapper = _mapper;
        }
        public async Task<OrganizationVM> Handle(GetOrganizationQuery request, CancellationToken cancellationToken)
        {
            var entity = await context.Organizations.FirstOrDefaultAsync(entity => entity.Id == request.Id,cancellationToken);
            if (entity == null || entity.Id != request.Id)
            {
                throw new NotFoundException(nameof(Organization), request.Id);
            }
            return mapper.Map<OrganizationVM>(entity);
        }
    }
}
