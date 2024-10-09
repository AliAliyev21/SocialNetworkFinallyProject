namespace SocialNetworkProject.Entities
{
    public class LiveChat
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User? User { get; set; }

        public string? Content { get; set; }
        public DateTime SentAt { get; set; }
        public DateTime ChatStartedAt { get;  set; }
        public bool IsActive { get;  set; }
    }

}
