namespace TeamHelper.Domain
{
    public class Coach
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public Organization Organization { get; set; }
    }
}
