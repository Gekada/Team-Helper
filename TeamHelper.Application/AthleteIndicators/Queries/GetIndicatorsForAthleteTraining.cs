using TeamHelper.Application.Interfaces;
using TeamHelper.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TeamHelper.Application.Common.Mapping;
using AutoMapper;
using AutoMapper.QueryableExtensions;

namespace TeamHelper.Application.AthleteIndicators.Queries
{
    using AthleteIndicators = Domain.AthleteIndicators;
    using IndicatorsData = Domain.IndicatorsData;

    public class AthleteIndicatorsDataLookupDTO : IMapWith<IndicatorsData>
    {
        public Guid Id { get; set; }
        public string Pulse { get; set; }
        public string Temperature { get; set; }
        public string BloodPressure { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<IndicatorsData, AthleteIndicatorsDataLookupDTO>();
        }
    }

    public class AthleteIndicatorsDataListVM
    {
        public IList<AthleteIndicatorsDataLookupDTO> Indicators { get; set; }
    }

    public class GetIndicatorsForAthleteListQuery : IRequest<AthleteIndicatorsDataListVM>
    {
        public Guid TrainingId { get; set; }
        public Guid AthleteId { get; set; }
    }
    public class GetIndicatorsForAthleteQueryHandler : IRequestHandler<GetIndicatorsForAthleteListQuery, AthleteIndicatorsDataListVM>
    {
        private readonly ITeamHelperDBContext context;
        private readonly IMapper mapper;
        public GetIndicatorsForAthleteQueryHandler(ITeamHelperDBContext _context, IMapper _mapper)
        {
            context = _context;
            mapper = _mapper;
        }
        public async Task<AthleteIndicatorsDataListVM> Handle(GetIndicatorsForAthleteListQuery request, CancellationToken cancellationToken)
        {
            context.Trainings.Load();
            context.Athletes.Load();
            context.AthleteIndicators.Load();
            context.IndicatorsDatas.Load();
            var indicators = await context.IndicatorsDatas.Where(node => node.AthleteIndicators.Training.Id == request.TrainingId && node.AthleteIndicators.Athlete.Id == request.AthleteId).ProjectTo<AthleteIndicatorsDataLookupDTO>(mapper.ConfigurationProvider).ToListAsync();
            return new AthleteIndicatorsDataListVM { Indicators = indicators };
        }
    }
}
