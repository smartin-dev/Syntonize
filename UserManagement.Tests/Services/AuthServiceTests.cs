using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Data.Models;
using UserManagement.Data.Repositories;
using UserManagement.Services;

namespace UserManagement.Tests.Services
{

    [TestClass]
    public class AuthServiceTests
    {
        private Mock<IUserRepository> _userRepositoryMock;
        private AuthService _authService;

        [TestInitialize]
        public void Setup()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _authService = new AuthService();
        }

        [TestMethod]
        public void Authenticate_ValidCredentials_ReturnsUser()
        {
            // Arrange
            var email = "test@example.com";
            var password = "password";
            var salt = PasswordHasher.GenerateSalt();
            var hashedPassword = PasswordHasher.ComputeHash(password, salt);
            var user = new User { Email = email, PasswordHash = hashedPassword, Salt = salt };
            _userRepositoryMock.Setup(repo => repo.GetByEmail(email)).Returns(user);

            // Act
            var authenticatedUser = _authService.Authenticate(email, password);

            // Assert
            Assert.IsNotNull(authenticatedUser);
            Assert.AreEqual(email, authenticatedUser.Email);
        }



    }
}
