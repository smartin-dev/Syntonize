using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Web;
using UserManagement.Data.Models;
using UserManagement.Data.Repositories;

namespace UserManagement.Services
{
    public class AuthService
    {
        private readonly IUserRepository _userRepository;

        public AuthService()
        {
            _userRepository = new UserRepository();
        }

        /// <summary>
        /// Authenticates a user using their email and password.
        /// </summary>
        /// <param name="email">The user's email address.</param>
        /// <param name="password">The user's password.</param>
        /// <returns>The authenticated user if successful, null otherwise.</returns>
        public User Authenticate(string email, string password)
        {
            // Retrieve the user by email from the repository
            var user = _userRepository.GetByEmail(email);

            

            // Check if user exists and if the password is correct
            if (user != null && VerifyPassword(password, user.PasswordHash, user.Salt))
            {
                // Authentication successful
                return user;
            }

            // Authentication failed
            return null;
        }

        internal bool IsEmailRegistered(string email)
        {
            return _userRepository.GetByEmail(email) != null;
        }


        // Helper method to verify the password
        private bool VerifyPassword(string password, byte[] storedHash, byte[] salt)
        {
            // Compute hash of the provided password using the stored salt
            var hashedPassword = PasswordHasher.ComputeHash(password, salt);

            // Compare the computed hash with the stored hash
            return hashedPassword.SequenceEqual(storedHash);
        }

        // Helper method to generate JWT token
        public  string GenerateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(ConfigurationManager.AppSettings["JwtSecret"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
            new Claim(ClaimTypes.Name, user.Name),
            new Claim(ClaimTypes.Email, user.Email),
                    // Add more claims as needed
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }



    }
}