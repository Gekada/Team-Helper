
namespace TeamHelper.Domain
{
    public class Athlete
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public Organization? Organization { get; set; }
        public ICollection<AthleteIndicators> AthleteIndicators { get; set; } = new List<AthleteIndicators>();
    }
}
