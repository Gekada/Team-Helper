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
    public class IndicatorsLookupDTO : IMapWith<AthleteIndicators>
    {
        public Guid Id { get; set; }
        public List<IndicatorsData> indicatorsDatas { get; set; }
        public Training Training { get; set; }
        public Athlete Athlete { get; set; }
        public Gear Gear { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<AthleteIndicators, IndicatorsLookupDTO>();
        }
    }

    public class IndicatorsListVM
    {
        public IList<IndicatorsLookupDTO> Indicators { get; set; }
    }

    public class GetIndicatorsListQuery : IRequest<IndicatorsListVM>
    {
    }
    public class GetIndicatorsListQueryHandler : IRequestHandler<GetIndicatorsListQuery, IndicatorsListVM>
    {
        private readonly ITeamHelperDBContext context;
        private readonly IMapper mapper;
        public GetIndicatorsListQueryHandler(ITeamHelperDBContext _context, IMapper _mapper)
        {
            context = _context;
            mapper = _mapper;
        }
        public async Task<IndicatorsListVM> Handle(GetIndicatorsListQuery request, CancellationToken cancellationToken)
        {
            var indicators = await context.AthleteIndicators.ProjectTo<IndicatorsLookupDTO>(mapper.ConfigurationProvider).ToListAsync();
            return new IndicatorsListVM { Indicators = indicators };
        }
    }
}
