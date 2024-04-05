using TeamHelper.Application.Interfaces;
using TeamHelper.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TeamHelper.Application.Common.Exceptions;
using TeamHelper.Application.Common.Mapping;
using AutoMapper;

namespace TeamHelper.Application.Athletes.Queries
{
    public class AthleteVM : IMapWith<Athlete>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public Organization Organization { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Athlete, AthleteVM>();
        }
    }
    public class GetAthleteQuery : IRequest<AthleteVM>
    {
        public Guid Id { get; set; }
    }
    public class GetAthleteQueryHandler : IRequestHandler<GetAthleteQuery, AthleteVM>
    {
        private readonly ITeamHelperDBContext context;
        private readonly IMapper mapper;
        public GetAthleteQueryHandler(ITeamHelperDBContext _context, IMapper _mapper)
        {
            context = _context;
            mapper = _mapper;
        }
        public async Task<AthleteVM> Handle(GetAthleteQuery request, CancellationToken cancellationToken)
        {
            var entity = await context.Athletes.FirstOrDefaultAsync(entity => entity.Id == request.Id, cancellationToken);
            if (entity == null || entity.Id != request.Id)
            {
                throw new NotFoundException(nameof(Athlete), request.Id);
            }
            return mapper.Map<AthleteVM>(entity);
        }
    }
}
