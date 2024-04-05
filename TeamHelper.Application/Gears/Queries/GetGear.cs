using TeamHelper.Application.Interfaces;
using TeamHelper.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TeamHelper.Application.Common.Exceptions;
using TeamHelper.Application.Common.Mapping;
using AutoMapper;

namespace TeamHelper.Application.Gears.Queries
{
    using AthleteIndicators = Domain.AthleteIndicators;
    public class GearVM : IMapWith<Gear>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public AthleteIndicators AthleteIndicators { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Gear, GearVM>();
        }
    }
    public class GetGearQuery : IRequest<GearVM>
    {
        public Guid Id { get; set; }
    }
    public class GetGearQueryHandler : IRequestHandler<GetGearQuery, GearVM>
    {
        private readonly ITeamHelperDBContext context;
        private readonly IMapper mapper;
        public GetGearQueryHandler(ITeamHelperDBContext _context, IMapper _mapper)
        {
            context = _context;
            mapper = _mapper;
        }
        public async Task<GearVM> Handle(GetGearQuery request, CancellationToken cancellationToken)
        {
            var entity = await context.Gears.FirstOrDefaultAsync(entity => entity.Id == request.Id, cancellationToken);
            if (entity == null || entity.Id != request.Id)
            {
                throw new NotFoundException(nameof(Gear), request.Id);
            }
            return mapper.Map<GearVM>(entity);
        }
    }
}
