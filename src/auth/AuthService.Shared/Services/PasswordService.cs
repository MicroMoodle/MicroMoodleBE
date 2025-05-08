using System;
using System.Security.Cryptography;
using System.Text;

using IMDB_BE.Common;

using Microsoft.Extensions.Options;

namespace IMDB_BE.Shared
{
    public class PasswordService
    {
        private const int SaltSize = 16; // 128-bit
        private const int KeySize = 32; // 256-bit
        private const int Iterations = 10000;
        private readonly PasswordSettings _passwordSettings;

        public PasswordService(IOptions<PasswordSettings> passwordSettings)
        {
            _passwordSettings = passwordSettings.Value;
        }

        public string HashPassword(string password)
        {
            var salt = new byte[SaltSize];
            RandomNumberGenerator.Fill(salt);

            using var rfc2898 = new Rfc2898DeriveBytes(password, salt, Iterations, HashAlgorithmName.SHA256);
            var hash = rfc2898.GetBytes(KeySize);

            var hashBytes = new byte[SaltSize + KeySize];
            Array.Copy(salt, 0, hashBytes, 0, SaltSize);
            Array.Copy(hash, 0, hashBytes, SaltSize, KeySize);

            return Convert.ToBase64String(hashBytes);
        }

        public bool VerifyPassword(string password, string hashedPassword)
        {
            var hashBytes = Convert.FromBase64String(hashedPassword);

            var salt = new byte[SaltSize];
            Array.Copy(hashBytes, 0, salt, 0, SaltSize);

            using var rfc2898 = new Rfc2898DeriveBytes(password, salt, Iterations, HashAlgorithmName.SHA256);
            var hash = rfc2898.GetBytes(KeySize);

            for (int i = 0; i < KeySize; i++)
            {
                if (hashBytes[SaltSize + i] != hash[i])
                {
                    return false;
                }
            }

            return true;
        }

        public string DecryptPassword(string encryptedPassword)
        {
            using var rsa = RSA.Create();

            try
            {
                var encryptedData = Convert.FromBase64String(encryptedPassword);

                var privateKeyBase64 = _passwordSettings.PrivateKey.Replace("-----BEGIN RSA PRIVATE KEY-----", "")
                    .Replace("-----END RSA PRIVATE KEY-----", "").Replace("\n", "").Trim();

                var privateKeyBytes = Convert.FromBase64String(privateKeyBase64);

                rsa.ImportRSAPrivateKey(privateKeyBytes, out _);

                var decryptedData = rsa.Decrypt(encryptedData, RSAEncryptionPadding.Pkcs1);

                return Encoding.UTF8.GetString(decryptedData);
            }
            catch (Exception ex)
            {
                throw new Exception($"Decryption failed: {ex.Message}", ex);
            }
        }
    }
}
