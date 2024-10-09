using System.ComponentModel.DataAnnotations.Schema;

namespace SocialNetworkProject.Entities
{
    public class Event
    {
        public int Id { get; set; }

        public string? Category { get; set; }
        public string? EventName { get; set; }
        public string? EventLocation { get; set; }
        public DateTime EventDate { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public string? Description { get; set; }

        public string? ImageUrl { get; set; }  
    }

}
