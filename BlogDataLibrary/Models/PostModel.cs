using System;

namespace BlogDataLibrary.Models
{
    public class PostModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }           // maps from p.Content AS Body
        public DateTime DateCreated { get; set; }  // maps from p.DateCreated
        public string Username { get; set; }       // maps from u.Username
        public string FirstName { get; set; }      // maps from u.FirstName
        public string LastName { get; set; }       // maps from u.LastName
    }
}
