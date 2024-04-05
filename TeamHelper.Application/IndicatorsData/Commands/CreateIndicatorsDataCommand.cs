using TeamHelper.Application.Interfaces;
using MediatR;
using TeamHelper.Application.Common.Exceptions;
using Microsoft.EntityFrameworkCore;
namespace TeamHelper.Application.IndicatorsData.Commands
{
    using AthleteIndicators = Domain.AthleteIndicators;
    using IndicatorsData = Domain.IndicatorsData;
    public class CreateIndicatorsDataCommand : IRequest<Guid>
    {
        public string Pulse { get; set; }
        public string Temperature { get; set; }
        public string BloodPressure { get; set; }
        public Guid IndicatorsId { get; set; }
    }
    public class CreateIndicatorsDataCommandHandler : IRequestHandler<CreateIndicatorsDataCommand, Guid>
    {
        private readonly ITeamHelperDBContext context;
        public CreateIndicatorsDataCommandHandler(ITeamHelperDBContext _context) => context = _context;
        public async Task<Guid> Handle(CreateIndicatorsDataCommand request, CancellationToken cancellationToken)
        {
            var indicators = await context.AthleteIndicators.FirstOrDefaultAsync(node => node.Id == request.IndicatorsId);
            if (indicators == null || indicators.Id != request.IndicatorsId)
            {
                throw new NotFoundException(nameof(AthleteIndicators), request.IndicatorsId);
            }
            var entity = new IndicatorsData
            {
                Id = Guid.NewGuid(),
                Pulse = request.Pulse,
                Temperature = request.Temperature,
                BloodPressure = request.BloodPressure,
                AthleteIndicators = indicators
            };
            await context.IndicatorsDatas.AddAsync(entity, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);
            return entity.Id;
        }
    }
}
