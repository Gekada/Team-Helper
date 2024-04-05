using TeamHelper.Application.Interfaces;
using TeamHelper.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TeamHelper.Application.Common.Mapping;
using AutoMapper;
using AutoMapper.QueryableExtensions;

namespace TeamHelper.Application.Teams.Queries
{
    public class CoachesTeamLookupDTO : IMapWith<Team>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int MembNumber { get; set; }
        public ICollection<Athlete> Athlete { get; set; }
        public Coach Coach { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Team, CoachesTeamLookupDTO>();
        }
    }

    public class CoachesTeamListVM
    {
        public IList<CoachesTeamLookupDTO> Teams { get; set; }
    }

    public class GetCoachesTeamsQuery : IRequest<CoachesTeamListVM>
    {
        public Guid CoachId { get; set; }
    }
    public class GetCoachesTeamsQueryHandler : IRequestHandler<GetCoachesTeamsQuery, CoachesTeamListVM>
    {
        private readonly ITeamHelperDBContext context;
        private readonly IMapper mapper;
        public GetCoachesTeamsQueryHandler(ITeamHelperDBContext _context, IMapper _mapper)
        {
            context = _context;
            mapper = _mapper;
        }
        public async Task<CoachesTeamListVM> Handle(GetCoachesTeamsQuery request, CancellationToken cancellationToken)
        {
            var teams = await context.Teams.Where(node => node.Coach.Id == request.CoachId).ProjectTo<CoachesTeamLookupDTO>(mapper.ConfigurationProvider).ToListAsync();
            return new CoachesTeamListVM { Teams = teams };
        }
    }
}
