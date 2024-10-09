namespace SocialNetworkProject.Entities
{
    public class Weather
    {
        public int Id { get; set; }
        public string? City { get; set; }
        public float Temperature { get; set; }
        public string? Description { get; set; }
        public DateTime Date { get; set; }
        public string? Condition { get; set; }
    }

}
