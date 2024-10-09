using Microsoft.AspNetCore.Mvc;
using SocialNetworkProject.Entities;
using System.Collections.Generic;
using System.Linq;

namespace SocialNetworkProject.Controllers
{
    public class MessagesController : Controller
    {
        // This is just a sample; in a real application, you would fetch this from a database
        private static List<Message> messages = new List<Message>
        {
            new Message { Id = 1, Content = "Hello, dear I want to talk to you?", SentAt = DateTime.Now, SenderId = 1, ReceiverId = 2 },
            new Message { Id = 2, Content = "How can I cooperate with you?", SentAt = DateTime.Now, SenderId = 2, ReceiverId = 1 },
            // Add more static messages for testing
        };

        public IActionResult Index(int friendId)
        {
            var userId = 1; // Replace with the current user's ID
            var userMessages = messages
                .Where(m => (m.SenderId == userId && m.ReceiverId == friendId) || (m.SenderId == friendId && m.ReceiverId == userId))
                .ToList();

            return View(userMessages);
        }

        [HttpPost]
        public IActionResult SendMessage(int receiverId, string content)
        {
            if (!string.IsNullOrEmpty(content))
            {
                var newMessage = new Message
                {
                    Id = messages.Count + 1,
                    Content = content,
                    SentAt = DateTime.Now,
                    SenderId = 1, // Replace with the current user's ID
                    ReceiverId = receiverId
                };
                messages.Add(newMessage);
            }

            return RedirectToAction("Index", new { friendId = receiverId });
        }
    }
}
