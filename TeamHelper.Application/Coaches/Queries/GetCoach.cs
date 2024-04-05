using TeamHelper.Application.Interfaces;
using TeamHelper.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TeamHelper.Application.Common.Exceptions;
using TeamHelper.Application.Common.Mapping;
using AutoMapper;

namespace TeamHelper.Application.Coaches.Queries
{
    public class CoachVM : IMapWith<Coach>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public Organization Organization { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Coach, CoachVM>();
        }
    }
    public class GetCoachQuery : IRequest<CoachVM>
    {
        public Guid Id { get; set; }
    }
    public class GetCoachQueryHandler : IRequestHandler<GetCoachQuery, CoachVM>
    {
        private readonly ITeamHelperDBContext context;
        private readonly IMapper mapper;
        public GetCoachQueryHandler(ITeamHelperDBContext _context, IMapper _mapper)
        {
            context = _context;
            mapper = _mapper;
        }
        public async Task<CoachVM> Handle(GetCoachQuery request, CancellationToken cancellationToken)
        {
            var entity = await context.Coaches.FirstOrDefaultAsync(entity => entity.Id == request.Id, cancellationToken);
            if (entity == null || entity.Id != request.Id)
            {
                throw new NotFoundException(nameof(Coach), request.Id);
            }
            return mapper.Map<CoachVM>(entity);
        }
    }
}
