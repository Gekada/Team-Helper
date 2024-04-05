using TeamHelper.Application.Interfaces;
using TeamHelper.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TeamHelper.Application.Common.Exceptions;
using TeamHelper.Application.Common.Mapping;
using AutoMapper;

namespace TeamHelper.Application.AthleteIndicators.Queries
{
    using AthleteIndicators = Domain.AthleteIndicators;
    using IndicatorsData = Domain.IndicatorsData;
    public class IndicatorsVM : IMapWith<AthleteIndicators>
    {
        public Guid Id { get; set; }
        public List<IndicatorsData> indicatorsDatas { get; set; }
        public Training Training { get; set; }
        public Athlete Athlete { get; set; }
        public Gear Gear { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<AthleteIndicators, IndicatorsVM>();
        }
    }
    public class GetIndicatorsQuery : IRequest<IndicatorsVM>
    {
        public Guid Id { get; set; }
    }
    public class GetIndicatorsQueryHandler : IRequestHandler<GetIndicatorsQuery, IndicatorsVM>
    {
        private readonly ITeamHelperDBContext context;
        private readonly IMapper mapper;
        public GetIndicatorsQueryHandler(ITeamHelperDBContext _context, IMapper _mapper)
        {
            context = _context;
            mapper = _mapper;
        }
        public async Task<IndicatorsVM> Handle(GetIndicatorsQuery request, CancellationToken cancellationToken)
        {
            var entity = await context.AthleteIndicators.FirstOrDefaultAsync(entity => entity.Id == request.Id, cancellationToken);
            if (entity == null || entity.Id != request.Id)
            {
                throw new NotFoundException(nameof(AthleteIndicators), request.Id);
            }
            return mapper.Map<IndicatorsVM>(entity);
        }
    }
}
