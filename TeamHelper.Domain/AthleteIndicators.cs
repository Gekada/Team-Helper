namespace TeamHelper.Domain
{
    public class AthleteIndicators
    {
        public Guid Id { get; set; }
        public IEnumerable<IndicatorsData> IndicatorsData { get; set; } = new List<IndicatorsData>();
        public Training Training { get; set; }
        public Athlete Athlete { get; set; }
        public Guid GearId { get; set; }
        public Gear Gear { get; set; }
    }
}
