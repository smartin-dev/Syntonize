using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace UserManagement.Services
{
    public static class PasswordHasher
    {
        private const int SaltSize = 32; // Size of salt in bytes
        private const int HashSize = 32; // Size of hash in bytes
        private const int Iterations = 10000; // Number of iterations for the PBKDF2 algorithm

        /// <summary>
        /// Generates a salt for password hashing.
        /// </summary>
        /// <returns>A byte array representing the generated salt.</returns>
        public static byte[] GenerateSalt()
        {
            byte[] salt = new byte[SaltSize];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(salt);
            }
            return salt;
        }

        /// <summary>
        /// Computes the hash of the provided password using the PBKDF2 algorithm with HMAC-SHA256.
        /// </summary>
        /// <param name="password">The password to hash.</param>
        /// <param name="salt">The salt used for hashing.</param>
        /// <returns>A byte array representing the computed hash.</returns>
        public static byte[] ComputeHash(string password, byte[] salt)
        {
            using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, Iterations, HashAlgorithmName.SHA256))
            {
                return pbkdf2.GetBytes(HashSize);
            }
        }

        /// <summary>
        /// Verifies whether the provided password matches the hashed password.
        /// </summary>
        /// <param name="password">The password to verify.</param>
        /// <param name="storedHash">The stored hash of the original password.</param>
        /// <param name="salt">The salt used for hashing.</param>
        /// <returns>True if the password matches the hashed password, false otherwise.</returns>
        public static bool VerifyPassword(string password, byte[] storedHash, byte[] salt)
        {
            byte[] computedHash = ComputeHash(password, salt);
            return CompareByteArrays(storedHash, computedHash);
        }

        /// <summary>
        /// Hashes the provided password using a random salt and returns the hashed password and salt.
        /// </summary>
        /// <param name="password">The password to hash.</param>
        /// <param name="salt">The generated salt used for hashing.</param>
        /// <returns>A byte array representing the hashed password.</returns>
        public static byte[] HashPassword(string password, out byte[] salt)
        {
            salt = GenerateSalt();
            return ComputeHash(password, salt);
        }

        // Helper method to compare byte arrays
        private static bool CompareByteArrays(byte[] array1, byte[] array2)
        {
            if (array1 == null || array2 == null || array1.Length != array2.Length)
                return false;

            bool areEqual = true;
            for (int i = 0; i < array1.Length; i++)
            {
                areEqual &= (array1[i] == array2[i]);
            }
            return areEqual;
        }
    }

}