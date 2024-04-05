using TeamHelper.Application.Interfaces;
using TeamHelper.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TeamHelper.Application.Common.Exceptions;
using TeamHelper.Application.Common.Mapping;
using AutoMapper;
using AutoMapper.QueryableExtensions;

namespace TeamHelper.Application.Organizations.Queries
{
    public class OrganizationLookupDTO : IMapWith<Organization>
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
            profile.CreateMap<Organization, OrganizationLookupDTO>();
        }
    }
    public class OrganizationListVM
    {
        public IList<OrganizationLookupDTO> Organizations {get; set; }
    }
    public class GetOrganizationsQuery: IRequest<OrganizationListVM>
    {

    }
    public class GetOrganizationsQueryHandler: IRequestHandler<GetOrganizationsQuery, OrganizationListVM>
    {
        private readonly ITeamHelperDBContext context;
        private readonly IMapper mapper;
        public GetOrganizationsQueryHandler(ITeamHelperDBContext _context, IMapper _mapper)
        {
            context = _context;
            mapper = _mapper;
        }
        public async Task<OrganizationListVM> Handle(GetOrganizationsQuery request, CancellationToken cancellationToken)
        {
            var organizations = await context.Organizations.ProjectTo<OrganizationLookupDTO>(mapper.ConfigurationProvider).ToListAsync();
            return new OrganizationListVM { Organizations = organizations };
        }
    }
}
