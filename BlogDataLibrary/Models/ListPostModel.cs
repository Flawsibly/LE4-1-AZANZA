using System;

namespace BlogDataLibrary.Models
{
    public class ListPostModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Username { get; set; }
        public DateTime DateCreated { get; set; }
        public string BodyPreview { get; set; }
    }
}
