using TeamHelper.Application.Interfaces;
using TeamHelper.Domain;
using MediatR;
using TeamHelper.Application.Common.Exceptions;

namespace TeamHelper.Application.Trainings.Commands
{
    public class DeleteTrainingCommand : IRequest
    {
        public Guid Id { get; set; }
    }
    public class DeleteTrainingCommandHandler : IRequestHandler<DeleteTrainingCommand>
    {
        private readonly ITeamHelperDBContext context;
        public DeleteTrainingCommandHandler(ITeamHelperDBContext _context) => context = _context;
        public async Task<Unit> Handle(DeleteTrainingCommand request, CancellationToken cancellationToken)
        {
            var entity = await context.Trainings.FindAsync(new object[] { request.Id }, cancellationToken);
            if (entity == null || entity.Id != request.Id)
            {
                throw new NotFoundException(nameof(Training), request.Id);
            }
            context.Trainings.Remove(entity);
            await context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}