using System.ComponentModel.DataAnnotations.Schema;

namespace SocialNetworkProject.Entities
{
    public class Message
    {
        public int Id { get; set; }
        public string? Content { get; set; }
        public DateTime SentAt { get; set; }

        public int SenderId { get; set; }
        [ForeignKey("SenderId")]
        public User? Sender { get; set; }

        public int ReceiverId { get; set; }
        [ForeignKey("ReceiverId")]
        public User? Receiver { get; set; }
    }

}
