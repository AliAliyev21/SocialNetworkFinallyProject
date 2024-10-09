using System.ComponentModel.DataAnnotations;

namespace SocialNetworkProject.Entities
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public DateTime Birthday { get; set; }
        public string? Occupation { get; set; }
        public string? Phone { get; set; }
        public string? Gender { get; set; }
        public string? RelationshipStatus { get; set; }
        public string? BloodGroup { get; set; }
        public string? Website { get; set; }

        public ICollection<Friend>? Friends { get; set; }
        public ICollection<Message>? SentMessages { get; set; }
        public ICollection<Message>? ReceivedMessages { get; set; }
        public ICollection<Event>? Events { get; set; }

        public User()
        {
            Friends = new HashSet<Friend>();
            SentMessages = new HashSet<Message>();
            ReceivedMessages = new HashSet<Message>();
            Events = new HashSet<Event>();
        }
    }
}
