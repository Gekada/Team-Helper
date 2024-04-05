using TeamHelper.Application.Interfaces;
using MediatR;
using TeamHelper.Application.Common.Exceptions;

namespace TeamHelper.Application.IndicatorsData.Commands
{
    using IndicatorsData = Domain.IndicatorsData;
    public class DeleteIndicatorsDataCommand : IRequest
    {
        public Guid Id { get; set; }
    }
    public class DeleteIndicatorsDataCommandHandler : IRequestHandler<DeleteIndicatorsDataCommand>
    {
        private readonly ITeamHelperDBContext context;
        public DeleteIndicatorsDataCommandHandler(ITeamHelperDBContext _context) => context = _context;
        public async Task<Unit> Handle(DeleteIndicatorsDataCommand request, CancellationToken cancellationToken)
        {
            var entity = await context.IndicatorsDatas.FindAsync(new object[] { request.Id }, cancellationToken);
            if (entity == null || entity.Id != request.Id)
            {
                throw new NotFoundException(nameof(IndicatorsData), request.Id);
            }
            context.IndicatorsDatas.Remove(entity);
            await context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}