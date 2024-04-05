using TeamHelper.Application.Interfaces;
using TeamHelper.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TeamHelper.Application.Common.Mapping;
using AutoMapper;
using AutoMapper.QueryableExtensions;

namespace TeamHelper.Application.Teams.Queries
{
    public class TeamLookupDTO : IMapWith<Team>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int MembNumber { get; set; }
        public ICollection<Athlete> Athlete { get; set; }
        public Coach Coach { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Team, TeamLookupDTO>();
        }
    }

    public class TeamListVM
    {
        public IList<TeamLookupDTO> Teams { get; set; }
    }

    public class GetTeamsQuery : IRequest<TeamListVM>
    {
    }
    public class GetTeamsQueryHandler : IRequestHandler<GetTeamsQuery, TeamListVM>
    {
        private readonly ITeamHelperDBContext context;
        private readonly IMapper mapper;
        public GetTeamsQueryHandler(ITeamHelperDBContext _context, IMapper _mapper)
        {
            context = _context;
            mapper = _mapper;
        }
        public async Task<TeamListVM> Handle(GetTeamsQuery request, CancellationToken cancellationToken)
        {
            var teams = await context.Teams.ProjectTo<TeamLookupDTO>(mapper.ConfigurationProvider).ToListAsync();
            return new TeamListVM { Teams = teams };
        }
    }
}
