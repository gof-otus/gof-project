using Finik.AuthService.Contracts;
using Finik.AuthService.Core;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;

namespace Finik.AuthService.Services
{
    public class SaltedPasswordManager : IPasswordManager
    {
        const int KeySize = 32;
        const int IterationsCount = 100000;
        public HashedPassword HashPassword(string password)
        {
            var salt = RandomNumberGenerator.GetBytes(KeySize);

            var hash = GetHash(password, salt);

            return new HashedPassword(
                Convert.ToBase64String(hash),
                Convert.ToBase64String(salt));
        }

        public bool IsPasswordValid(string password, HashedPassword hashedPassword)
        {
            var hashToCompare = GetHash(password, Convert.FromBase64String(hashedPassword.Salt));
            return CryptographicOperations.FixedTimeEquals(hashToCompare, Convert.FromBase64String(hashedPassword.Hash));
        }

        private static byte[] GetHash(string password, byte[] salt)
        {
            return KeyDerivation.Pbkdf2(
                password: password!,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: IterationsCount,
                numBytesRequested: KeySize);
        }
    }
}
