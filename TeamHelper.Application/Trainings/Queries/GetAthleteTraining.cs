using TeamHelper.Application.Interfaces;
using TeamHelper.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TeamHelper.Application.Common.Mapping;
using AutoMapper;
using AutoMapper.QueryableExtensions;

namespace TeamHelper.Application.Trainings.Queries
{
    public class GetAthleteTrainingsQuery : IRequest<TrainingListVM>
    {
        public Guid Id { get; set; }
    }
    public class GetAthleteTrainingsQueryHandler : IRequestHandler<GetAthleteTrainingsQuery, TrainingListVM>
    {
        private readonly ITeamHelperDBContext context;
        private readonly IMapper mapper;
        public GetAthleteTrainingsQueryHandler(ITeamHelperDBContext _context, IMapper _mapper)
        {
            context = _context;
            mapper = _mapper;
        }
        public async Task<TrainingListVM> Handle(GetAthleteTrainingsQuery request, CancellationToken cancellationToken)
        {
            context.Athletes.Load();
            var trainings = await context.Trainings.Where(node => node.Team.Athlete.First(node => node.Id == request.Id)!= null).ProjectTo<TrainingLookupDTO>(mapper.ConfigurationProvider).ToListAsync();
            return new TrainingListVM { Trainings = trainings };
        }
    }
}
