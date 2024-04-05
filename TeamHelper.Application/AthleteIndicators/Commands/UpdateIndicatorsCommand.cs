using TeamHelper.Application.Interfaces;
using TeamHelper.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TeamHelper.Application.Common.Exceptions;

namespace TeamHelper.Application.AthleteIndicators.Commands
{
    //Maybe delete
    using AthleteIndicators = Domain.AthleteIndicators;
    public class UpdateIndicatorsCommand : IRequest
    {
        public Guid Id { get; set; }

    }
    public class UpdateIndicatorsCommandHandler : IRequestHandler<UpdateIndicatorsCommand>
    {
        private readonly ITeamHelperDBContext context;
        public UpdateIndicatorsCommandHandler(ITeamHelperDBContext _context) => context = _context;
        public async Task<Unit> Handle(UpdateIndicatorsCommand request, CancellationToken cancellationToken)
        {
            var entity = await context.AthleteIndicators.FirstOrDefaultAsync(node => node.Id == request.Id, cancellationToken);
            if (entity == null || entity.Id != request.Id)
            {
                throw new NotFoundException(nameof(AthleteIndicators), request.Id);
            }
            await context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
