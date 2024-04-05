namespace TeamHelper.Domain
{
    public class Team
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int MembNumber { get; set; }
        public ICollection<Athlete> Athlete { get; set; } = new List<Athlete>();
        public Coach Coach { get; set; }
    }
}
