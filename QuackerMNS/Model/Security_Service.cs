using System.Security.Cryptography;

namespace QuackerMNS.Model
{
    public class Security_Service
    {
        public static string HashPassword(string password)
        {
            if (string.IsNullOrEmpty(password))
            {
                throw new ArgumentException("Le mot de passe ne peut pas être vide", nameof(password));
            }

            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public static string GenerateToken(int size = 32)
        {
            using (var randomNumberGenerator = new RNGCryptoServiceProvider())
            {
                var tokenBytes = new byte[size];
                randomNumberGenerator.GetBytes(tokenBytes);
                return Convert.ToBase64String(tokenBytes);
            }
        }
    }
}
