using TeamHelper.Application.Interfaces;
using TeamHelper.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TeamHelper.Application.Common.Mapping;
using AutoMapper;
using AutoMapper.QueryableExtensions;

namespace TeamHelper.Application.Coaches.Queries
{
    public class CoachLookupDTO : IMapWith<Coach>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public Organization Organization { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Coach, CoachLookupDTO>();
        }
    }

    public class CoachListVM
    {
        public IList<CoachLookupDTO> Coaches { get; set; }
    }

    public class GetCoachesQuery : IRequest<CoachListVM>
    {
    }
    public class GetCoachesQueryHandler : IRequestHandler<GetCoachesQuery, CoachListVM>
    {
        private readonly ITeamHelperDBContext context;
        private readonly IMapper mapper;
        public GetCoachesQueryHandler(ITeamHelperDBContext _context, IMapper _mapper)
        {
            context = _context;
            mapper = _mapper;
        }
        public async Task<CoachListVM> Handle(GetCoachesQuery request, CancellationToken cancellationToken)
        {
            var coaches = await context.Coaches.ProjectTo<CoachLookupDTO>(mapper.ConfigurationProvider).ToListAsync();
            return new CoachListVM { Coaches = coaches };
        }
    }
}
