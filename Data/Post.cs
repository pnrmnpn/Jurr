using System;

namespace Jurr.Data
{
    public class Post
    {
        public int PostId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Content { get; set; }
        public DateTime CreatedAt { get; set; }

        // FK
        public int AdminId { get; set; }
        public Admin Admin { get; set; } = null!;

        // Blog image URL under wwwroot/images/blog
        public string? ImageUrl { get; set; }
    }
}
