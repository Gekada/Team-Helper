using TeamHelper.Application.Interfaces;
using TeamHelper.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TeamHelper.Application.Common.Exceptions;

namespace TeamHelper.Application.AthleteIndicators.Commands
{
    public class CreateIndicatorsCommand : IRequest<Guid>
    {
        public Guid TrainingId { get; set; }
        public Guid GearId { get; set; }
        public Guid AthleteId { get; set; }
    }
    public class CreateIndicatorsCommandHandler : IRequestHandler<CreateIndicatorsCommand, Guid>
    {
        private readonly ITeamHelperDBContext context;
        public CreateIndicatorsCommandHandler(ITeamHelperDBContext _context) => context = _context;
        public async Task<Guid> Handle(CreateIndicatorsCommand request, CancellationToken cancellationToken)
        {
            var training = await context.Trainings.FirstOrDefaultAsync(node => node.Id == request.TrainingId, cancellationToken);
            if (training == null || training.Id != request.TrainingId)
            {
                throw new NotFoundException(nameof(Training), request.TrainingId);
            }
            var athlete = await context.Athletes.FirstOrDefaultAsync(node => node.Id == request.AthleteId, cancellationToken);
            if (athlete == null || athlete.Id != request.AthleteId)
            {
                throw new NotFoundException(nameof(Athlete), request.AthleteId);
            }
            var gear = await context.Gears.FirstOrDefaultAsync(node => node.Id == request.GearId, cancellationToken);
            if (gear == null || gear.Id != request.GearId)
            {
                throw new NotFoundException(nameof(Gear), request.GearId);
            }
            var entity = new Domain.AthleteIndicators
            {
                Training = training,
                Athlete = athlete,
                Gear = gear,
                GearId = request.GearId
            };
            await context.AthleteIndicators.AddAsync(entity, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);
            return entity.Id;
        }
    }
}
