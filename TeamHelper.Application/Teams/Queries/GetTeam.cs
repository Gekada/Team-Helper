using TeamHelper.Application.Interfaces;
using TeamHelper.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TeamHelper.Application.Common.Exceptions;
using TeamHelper.Application.Common.Mapping;
using AutoMapper;

namespace TeamHelper.Application.Teams.Queries
{
    public class TeamVM : IMapWith<Team>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int MembNumber { get; set; }
        public ICollection<Athlete> Athlete { get; set; }
        public Coach Coach { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Team, TeamVM>();
        }
    }
    public class GetTeamQuery : IRequest<TeamVM>
    {
        public Guid Id { get; set; }
    }
    public class GetTeamQueryHandler : IRequestHandler<GetTeamQuery, TeamVM>
    {
        private readonly ITeamHelperDBContext context;
        private readonly IMapper mapper;
        public GetTeamQueryHandler(ITeamHelperDBContext _context, IMapper _mapper)
        {
            context = _context;
            mapper = _mapper;
        }
        public async Task<TeamVM> Handle(GetTeamQuery request, CancellationToken cancellationToken)
        {
            context.Athletes.Load();
            var entity = await context.Teams.FirstOrDefaultAsync(entity => entity.Id == request.Id, cancellationToken);
            if (entity == null || entity.Id != request.Id)
            {
                throw new NotFoundException(nameof(Team), request.Id);
            }
            return mapper.Map<TeamVM>(entity);
        }
    }
}
