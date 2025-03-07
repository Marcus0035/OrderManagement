using Microsoft.AspNetCore.Identity;

namespace OrderManagement.Services
{
    public class LoginHash
    {
        private readonly PasswordHasher<string> _passwordHasher = new();

        public string HashPassword(string user, string password)
        {
            return _passwordHasher.HashPassword(user, password);
        }

        public PasswordVerificationResult VerifyHashedPassword(string user, string hashedPassword, string providedPassword)
        {
            return _passwordHasher.VerifyHashedPassword(user, hashedPassword, providedPassword);
        }

    }
}
