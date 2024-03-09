using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;

namespace IMGCloud.Utilities.PasswordHashExtension
{
    public static class PasswordHashExtension
    {
        public static string ToHashPassword(this string originalPassword)
        {
            byte[] salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            byte[] hashedBytes = KeyDerivation.Pbkdf2(
                password: originalPassword,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 256 / 8);
            return $"{Convert.ToBase64String(salt)}:{Convert.ToBase64String(hashedBytes)}";
        }

        public static bool VerifyPassword(this string originalPassword, string hashedPassword)
        {
            // Tách salt và hashed password từ chuỗi đã lưu trữ
            string[] parts = hashedPassword.Split(':');
            byte[] salt = Convert.FromBase64String(parts[0]);
            byte[] storedHash = Convert.FromBase64String(parts[1]);

            // Mã hóa mật khẩu cung cấp và so sánh với hashed password đã lưu
            byte[] providedHash = KeyDerivation.Pbkdf2(
                password: originalPassword,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 256 / 8);

            return ByteArraysEqual(storedHash, providedHash);
        }

        private static bool ByteArraysEqual(byte[] a, byte[] b)
        {
            if (a.Length != b.Length)
            {
                return false;
            }

            for (int i = 0; i < a.Length; i++)
            {
                if (a[i] != b[i])
                {
                    return false;
                }
            }
            return true;
        }
    }
}
