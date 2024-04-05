using TeamHelper.Application.Interfaces;
using TeamHelper.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TeamHelper.Application.Common.Mapping;
using AutoMapper;
using AutoMapper.QueryableExtensions;

namespace TeamHelper.Application.Trainings.Queries
{
    public class TrainingLookupDTO : IMapWith<Training>
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public string Location { get; set; }
        public string Duration { get; set; }
        public Team Team { get; set; }
        public bool IsInprocess { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Training, TrainingLookupDTO>();
        }
    }

    public class TrainingListVM
    {
        public IList<TrainingLookupDTO> Trainings { get; set; }
    }

    public class GetTrainingsQuery : IRequest<TrainingListVM>
    {
    }
    public class GetTrainingsQueryHandler : IRequestHandler<GetTrainingsQuery, TrainingListVM>
    {
        private readonly ITeamHelperDBContext context;
        private readonly IMapper mapper;
        public GetTrainingsQueryHandler(ITeamHelperDBContext _context, IMapper _mapper)
        {
            context = _context;
            mapper = _mapper;
        }
        public async Task<TrainingListVM> Handle(GetTrainingsQuery request, CancellationToken cancellationToken)
        {
            context.Athletes.Load();
            var trainings = await context.Trainings.ProjectTo<TrainingLookupDTO>(mapper.ConfigurationProvider).ToListAsync();
            return new TrainingListVM { Trainings = trainings };
        }
    }
}
