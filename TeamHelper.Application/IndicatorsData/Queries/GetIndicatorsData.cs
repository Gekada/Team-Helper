using TeamHelper.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TeamHelper.Application.Common.Exceptions;
using TeamHelper.Application.Common.Mapping;
using AutoMapper;

namespace TeamHelper.Application.IndicatorsData.Queries
{
    using IndicatorsData = Domain.IndicatorsData;
    using AthleteIndicators = Domain.AthleteIndicators;
    public class IndicatorsDataVM : IMapWith<IndicatorsData>
    {
        public Guid Id { get; set; }
        public string Pulse { get; set; }
        public string Temperature { get; set; }
        public string BloodPressure { get; set; }
        public AthleteIndicators AthleteIndicators { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<IndicatorsData, IndicatorsDataVM>();
        }
    }
    public class GetIndicatorsDataQuery : IRequest<IndicatorsDataVM>
    {
        public Guid Id { get; set; }
    }
    public class GetIndicatorsDataQueryHandler : IRequestHandler<GetIndicatorsDataQuery, IndicatorsDataVM>
    {
        private readonly ITeamHelperDBContext context;
        private readonly IMapper mapper;
        public GetIndicatorsDataQueryHandler(ITeamHelperDBContext _context, IMapper _mapper)
        {
            context = _context;
            mapper = _mapper;
        }
        public async Task<IndicatorsDataVM> Handle(GetIndicatorsDataQuery request, CancellationToken cancellationToken)
        {
            var entity = await context.IndicatorsDatas.FirstOrDefaultAsync(entity => entity.Id == request.Id, cancellationToken);
            if (entity == null || entity.Id != request.Id)
            {
                throw new NotFoundException(nameof(IndicatorsData), request.Id);
            }
            return mapper.Map<IndicatorsDataVM>(entity);
        }
    }
}
