using TeamHelper.Application.Interfaces;
using TeamHelper.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TeamHelper.Application.Common.Mapping;
using AutoMapper;
using AutoMapper.QueryableExtensions;

namespace TeamHelper.Application.Athletes.Queries
{
    using AthleteIndicators = Domain.AthleteIndicators;
    public class GearLookupDTO : IMapWith<Gear>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public AthleteIndicators AthleteIndicators { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Gear, GearLookupDTO>();
        }
    }

    public class GearListVM
    {
        public IList<GearLookupDTO> Gears { get; set; }
    }

    public class GetGearsQuery : IRequest<GearListVM>
    {
    }
    public class GetGearsQueryHandler : IRequestHandler<GetGearsQuery, GearListVM>
    {
        private readonly ITeamHelperDBContext context;
        private readonly IMapper mapper;
        public GetGearsQueryHandler(ITeamHelperDBContext _context, IMapper _mapper)
        {
            context = _context;
            mapper = _mapper;
        }
        public async Task<GearListVM> Handle(GetGearsQuery request, CancellationToken cancellationToken)
        {
            var gears = await context.Gears.ProjectTo<GearLookupDTO>(mapper.ConfigurationProvider).ToListAsync();
            return new GearListVM { Gears = gears };
        }
    }
}
