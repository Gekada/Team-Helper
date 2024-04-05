using TeamHelper.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TeamHelper.Application.Common.Mapping;
using AutoMapper;
using AutoMapper.QueryableExtensions;

namespace TeamHelper.Application.IndicatorsData.Queries
{
    using AthleteIndicators = Domain.AthleteIndicators;
    using IndicatorsData = Domain.IndicatorsData;
    public class IndicatorsDataLookupDTO : IMapWith<IndicatorsData>
    {
        public Guid Id { get; set; }
        public string Pulse { get; set; }
        public string Temperature { get; set; }
        public string BloodPressure { get; set; }
        public AthleteIndicators AthleteIndicators { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<IndicatorsData, IndicatorsDataLookupDTO>();
        }
    }

    public class IndicatorsDataListVM
    {
        public IList<IndicatorsDataLookupDTO> Indicators { get; set; }
    }

    public class GetIndicatorsDataListQuery : IRequest<IndicatorsDataListVM>
    {
    }
    public class GetIndicatorsDataListQueryHandler : IRequestHandler<GetIndicatorsDataListQuery, IndicatorsDataListVM>
    {
        private readonly ITeamHelperDBContext context;
        private readonly IMapper mapper;
        public GetIndicatorsDataListQueryHandler(ITeamHelperDBContext _context, IMapper _mapper)
        {
            context = _context;
            mapper = _mapper;
        }
        public async Task<IndicatorsDataListVM> Handle(GetIndicatorsDataListQuery request, CancellationToken cancellationToken)
        {
            var indicatorsData = await context.IndicatorsDatas.ProjectTo<IndicatorsDataLookupDTO>(mapper.ConfigurationProvider).ToListAsync();
            return new IndicatorsDataListVM { Indicators = indicatorsData };
        }
    }
}
