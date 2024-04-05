using TeamHelper.Application.Interfaces;
using TeamHelper.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TeamHelper.Application.Common.Exceptions;
using TeamHelper.Application.Common.Mapping;
using AutoMapper;

namespace TeamHelper.Application.Teams.Queries
{
    public class GetAthletesTeamQuery : IRequest<TeamVM>
    {
        public Guid Id { get; set; }
    }
    public class GetAthletesTeamQueryHandler : IRequestHandler<GetAthletesTeamQuery, TeamVM>
    {
        private readonly ITeamHelperDBContext context;
        private readonly IMapper mapper;
        public GetAthletesTeamQueryHandler(ITeamHelperDBContext _context, IMapper _mapper)
        {
            context = _context;
            mapper = _mapper;
        }
        public async Task<TeamVM> Handle(GetAthletesTeamQuery request, CancellationToken cancellationToken)
        {
            context.Athletes.Load();
            var entity = await context.Teams.FirstOrDefaultAsync(entity => entity.Athlete.First(node => node.Id == request.Id)!=null, cancellationToken);
            if (entity == null)
            {
                throw new NotFoundException(nameof(Team), request.Id);
            }
            return mapper.Map<TeamVM>(entity);
        }
    }
}
