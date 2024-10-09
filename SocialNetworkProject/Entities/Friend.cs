using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SocialNetworkProject.Entities
{
    public class Friend
    {
        [Key]
        public int Id { get; set; } 

        public int UserId { get; set; } 
        public string? FullName { get; set; }
        public int Likes { get; set; }
        public bool Following { get; set; }
        public bool Followers { get; set; }
        public int MutualFriends { get; set; }
        public string? ProfileImage { get; set; }

        [ForeignKey("UserId")] 
        public User? User { get; set; }
    }

}


