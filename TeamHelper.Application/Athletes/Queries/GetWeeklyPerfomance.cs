using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TeamHelper.Application.Common.Exceptions;
using TeamHelper.Application.Interfaces;
using TeamHelper.Domain;

namespace TeamHelper.Application.Athletes.Queries
{

    public class GetWeeklyPerfomanceQuery:IRequest<List<Training>>
    {
        public Guid AthleteId;
    }
    public class GetWeeklyPerfomanceQueryHandler: IRequestHandler<GetWeeklyPerfomanceQuery, List<Training>>
    {
        private readonly ITeamHelperDBContext context;
        public GetWeeklyPerfomanceQueryHandler(ITeamHelperDBContext _context, IMapper _mapper)
        {
            context = _context;
        }
        public async Task<List<Training>> Handle(GetWeeklyPerfomanceQuery request, CancellationToken cancellationToken)
        {
            var athlete = await context.Athletes.FirstOrDefaultAsync(node => node.Id == request.AthleteId, cancellationToken);
            if (athlete == null || athlete.Id != request.AthleteId)
            {
                throw new NotFoundException(nameof(Athlete), request.AthleteId);
            }
            var trainings = context.Trainings.Where(node => node.Team.Athlete.Contains(athlete)).ToList();
            return trainings;
        }
    }
}
