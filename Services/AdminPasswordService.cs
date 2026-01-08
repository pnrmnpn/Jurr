using Jurr.Data;
using Microsoft.AspNetCore.Identity;

namespace Jurr.Services
{
    public interface IAdminPasswordService
    {
        string HashPassword(Admin admin, string plainPassword);
        bool Verify(Admin admin, string plainPassword);
    }

    public class AdminPasswordService : IAdminPasswordService
    {
        private readonly PasswordHasher<Admin> _hasher = new();

        public string HashPassword(Admin admin, string plainPassword)
        {
            return _hasher.HashPassword(admin, plainPassword);
        }

        public bool Verify(Admin admin, string plainPassword)
        {
            var result = _hasher.VerifyHashedPassword(admin, admin.PasswordHash, plainPassword);
            return result != PasswordVerificationResult.Failed;
        }
    }
}
