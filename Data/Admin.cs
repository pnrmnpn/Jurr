using System;
using System.Collections.Generic;

namespace Jurr.Data
{
    public class Admin
    {
        public int AdminId { get; set; }
        public string UserName { get; set; } = null!;
        public string NormalizedUserName { get; set; } = null!;
        public string? Email { get; set; }
        public string? NormalizedEmail { get; set; }
        public string PasswordHash { get; set; } = null!;
        public bool IsActive { get; set; }
        public bool IsSuperAdmin { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? LastLoginAt { get; set; }

        public ICollection<Post> Posts { get; set; } = new List<Post>();
    }
}
