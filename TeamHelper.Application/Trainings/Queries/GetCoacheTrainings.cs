using TeamHelper.Application.Interfaces;
using TeamHelper.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TeamHelper.Application.Common.Mapping;
using AutoMapper;
using AutoMapper.QueryableExtensions;

namespace TeamHelper.Application.Trainings.Queries
{
    public class CoachesTrainingLookupDTO : IMapWith<Training>
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public string Location { get; set; }
        public string Duration { get; set; }
        public Team Team { get; set; }
        public bool IsInprocess { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Training, CoachesTrainingLookupDTO>();
        }
    }

    public class CoachesTrainingListVM
    {
        public IList<CoachesTrainingLookupDTO> Trainings { get; set; }
    }

    public class GetCoachesTrainingsQuery : IRequest<CoachesTrainingListVM>
    {
        public Guid Id { get; set;}
    }
    public class GetCoachesTrainingsQueryHandler : IRequestHandler<GetCoachesTrainingsQuery, CoachesTrainingListVM>
    {
        private readonly ITeamHelperDBContext context;
        private readonly IMapper mapper;
        public GetCoachesTrainingsQueryHandler(ITeamHelperDBContext _context, IMapper _mapper)
        {
            context = _context;
            mapper = _mapper;
        }
        public async Task<CoachesTrainingListVM> Handle(GetCoachesTrainingsQuery request, CancellationToken cancellationToken)
        {
            context.Trainings.Load();
            context.Teams.Load();
            context.Coaches.Load();
            var trainings = await context.Trainings.Where(node => node.Team.Coach.Id == request.Id).ProjectTo<CoachesTrainingLookupDTO>(mapper.ConfigurationProvider).ToListAsync();
            return new CoachesTrainingListVM { Trainings = trainings };
        }
    }
}
