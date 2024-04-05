namespace TeamHelper.Domain
{
    public class Training
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public string Location { get; set; }
        public string Duration { get; set; }
        public Team Team { get; set; }
        public bool IsInprocess { get; set; }
    }
}
