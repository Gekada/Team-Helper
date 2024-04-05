using TeamHelper.Application.Interfaces;
using MediatR;
using TeamHelper.Application.Common.Exceptions;

namespace TeamHelper.Application.AthleteIndicators.Commands
{
    using AthleteIndicators = Domain.AthleteIndicators;
    public class DeleteIndicatorsCommand : IRequest
    {
        public Guid Id { get; set; }
    }
    public class DeleteIndicatorsCommandHandler : IRequestHandler<DeleteIndicatorsCommand>
    {
        private readonly ITeamHelperDBContext context;
        public DeleteIndicatorsCommandHandler(ITeamHelperDBContext _context) => context = _context;
        public async Task<Unit> Handle(DeleteIndicatorsCommand request, CancellationToken cancellationToken)
        {
            var entity = await context.AthleteIndicators.FindAsync(new object[] { request.Id }, cancellationToken);
            if (entity == null || entity.Id != request.Id)
            {
                throw new NotFoundException(nameof(AthleteIndicators), request.Id);
            }
            context.AthleteIndicators.Remove(entity);
            await context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}