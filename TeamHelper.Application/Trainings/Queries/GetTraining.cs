using TeamHelper.Application.Interfaces;
using TeamHelper.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TeamHelper.Application.Common.Exceptions;
using TeamHelper.Application.Common.Mapping;
using AutoMapper;

namespace TeamHelper.Application.Trainings.Queries
{
    public class TrainingVM : IMapWith<Training>
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public string Location { get; set; }
        public string Duration { get; set; }
        public Team Team { get; set; }
        public bool IsInprocess { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Training, TrainingVM>();
        }
    }
    public class GetTrainingQuery : IRequest<TrainingVM>
    {
        public Guid Id { get; set; }
    }
    public class GetTrainingQueryHandler : IRequestHandler<GetTrainingQuery, TrainingVM>
    {
        private readonly ITeamHelperDBContext context;
        private readonly IMapper mapper;
        public GetTrainingQueryHandler(ITeamHelperDBContext _context, IMapper _mapper)
        {
            context = _context;
            mapper = _mapper;
        }
        public async Task<TrainingVM> Handle(GetTrainingQuery request, CancellationToken cancellationToken)
        {
            context.Teams.Load();
            context.Athletes.Load();
            var entity = await context.Trainings.FirstOrDefaultAsync(entity => entity.Id == request.Id, cancellationToken);
            if (entity == null || entity.Id != request.Id)
            {
                throw new NotFoundException(nameof(Training), request.Id);
            }
            return mapper.Map<TrainingVM>(entity);
        }
    }
}
