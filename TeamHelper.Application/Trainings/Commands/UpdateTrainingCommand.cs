using TeamHelper.Application.Interfaces;
using TeamHelper.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TeamHelper.Application.Common.Exceptions;

namespace TeamHelper.Application.Trainings.Commands
{
    public class UpdateTrainingCommand : IRequest
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public string Location { get; set; }
        public string Duration { get; set; }
    }
    public class UpdateTrainingCommandHandler : IRequestHandler<UpdateTrainingCommand>
    {
        private readonly ITeamHelperDBContext context;
        public UpdateTrainingCommandHandler(ITeamHelperDBContext _context) => context = _context;
        public async Task<Unit> Handle(UpdateTrainingCommand request, CancellationToken cancellationToken)
        {
            var entity = await context.Trainings.FirstOrDefaultAsync(node => node.Id == request.Id, cancellationToken);
            if (entity == null || entity.Id != request.Id)
            {
                throw new NotFoundException(nameof(Training), request.Id);
            }
            entity.Date = request.Date;
            entity.Location = request.Location;
            entity.Duration = request.Duration;
            await context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
