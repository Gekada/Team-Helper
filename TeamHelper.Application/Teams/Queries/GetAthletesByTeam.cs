using TeamHelper.Application.Interfaces;
using TeamHelper.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TeamHelper.Application.Common.Mapping;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using TeamHelper.Application.Athletes.Queries;
using TeamHelper.Application.Common.Exceptions;

namespace TeamHelper.Application.Teams.Queries
{
    public class GetAthletesByTeamQuery : IRequest<ICollection<Athlete>>
    {
        public Guid Id { get; set; }
    }
    public class GetAthletesByTeamQueryHandler : IRequestHandler<GetAthletesByTeamQuery, ICollection<Athlete>>
    {
        private readonly ITeamHelperDBContext context;
        private readonly IMapper mapper;
        public GetAthletesByTeamQueryHandler(ITeamHelperDBContext _context, IMapper _mapper)
        {
            context = _context;
            mapper = _mapper;
        }
        public async Task<ICollection<Athlete>> Handle(GetAthletesByTeamQuery request, CancellationToken cancellationToken)
        {
            var entity = await context.Teams.FirstOrDefaultAsync(entity => entity.Id == request.Id, cancellationToken);
            if (entity == null || entity.Id != request.Id)
            {
                throw new NotFoundException(nameof(Team), request.Id);
            }
            var athletes = entity.Athlete;
            return athletes;
        }
    }
}
