namespace TeamHelper.Domain
{
    public class Organization
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Adress { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public ICollection<Coach>? Coaches { get; set; } = new List<Coach>();
        public ICollection<Athlete>? Athletes { get; set; } = new List<Athlete>();
    }
}
