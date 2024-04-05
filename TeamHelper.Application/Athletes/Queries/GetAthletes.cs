using TeamHelper.Application.Interfaces;
using TeamHelper.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TeamHelper.Application.Common.Mapping;
using AutoMapper;
using AutoMapper.QueryableExtensions;

namespace TeamHelper.Application.Athletes.Queries
{
    public class AthleteLookupDTO : IMapWith<Athlete>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public Organization Organization { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Athlete, AthleteLookupDTO>();
        }
    }

    public class AthleteListVM
    {
        public IList<AthleteLookupDTO> Athletes { get; set; }
    }

    public class GetAthletesQuery : IRequest<AthleteListVM>
    {
    }
    public class GetAthletesQueryHandler : IRequestHandler<GetAthletesQuery, AthleteListVM>
    {
        private readonly ITeamHelperDBContext context;
        private readonly IMapper mapper;
        public GetAthletesQueryHandler(ITeamHelperDBContext _context, IMapper _mapper)
        {
            context = _context;
            mapper = _mapper;
        }
        public async Task<AthleteListVM> Handle(GetAthletesQuery request, CancellationToken cancellationToken)
        {
            var athletes = await context.Athletes.ProjectTo<AthleteLookupDTO>(mapper.ConfigurationProvider).ToListAsync();
            return new AthleteListVM { Athletes = athletes };
        }
    }
}
