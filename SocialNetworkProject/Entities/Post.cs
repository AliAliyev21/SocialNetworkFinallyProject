namespace SocialNetworkProject.Entities
{
    public class Post
    {
        public int Id { get; set; } // Unique identifier for the post
        public string? Message { get; set; } // Content of the post
        public string? ImagePath { get; set; } // Path to the uploaded image
        public string? VideoPath { get; set; } // Path to the uploaded video
        public DateTime CreatedAt { get; set; } // Date and time the post was created
        public int UserId { get; set; } // Foreign key to the user who created the post
        public virtual User? User { get; set; } // Navigation property to the User entity
        public bool IsHidden { get; set; }

       
    }
}
