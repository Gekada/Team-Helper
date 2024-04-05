using TeamHelper.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TeamHelper.Application.Common.Exceptions;

//Maybe delete
namespace TeamHelper.Application.IndicatorsData.Commands
{
    using IndicatorsData = Domain.IndicatorsData;
    public class UpdateIndicatorsDataCommand : IRequest
    {
        public Guid Id { get; set; }
        public string Pulse { get; set; }
        public string Temperature { get; set; }
        public string BloodPressure { get; set; }
    }
    public class UpdateIndicatorsDataCommandHandler : IRequestHandler<UpdateIndicatorsDataCommand>
    {
        private readonly ITeamHelperDBContext context;
        public UpdateIndicatorsDataCommandHandler(ITeamHelperDBContext _context) => context = _context;
        public async Task<Unit> Handle(UpdateIndicatorsDataCommand request, CancellationToken cancellationToken)
        {
            var entity = await context.IndicatorsDatas.FirstOrDefaultAsync(node => node.Id == request.Id, cancellationToken);
            if (entity == null || entity.Id != request.Id)
            {
                throw new NotFoundException(nameof(IndicatorsData), request.Id);
            }
            entity.Pulse = request.Pulse;
            entity.BloodPressure = request.BloodPressure;
            entity.Temperature = request.Temperature;
            await context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
