using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.App_Start;
using UserManagement.Data.Models;
using UserManagement.Data.Repositories;
using UserManagement.DTOs;
using UserManagement.Services;

namespace UserManagement.Tests
{
    [TestClass]
    public class UserServiceTest
    {
        private readonly UserService _userService;
        private readonly Mock<IUserRepository> _userRepositoryMock;

        public UserServiceTest()
        {
            // Initialize IUserService with the mock repository
            _userService = new UserService();
        }

        [TestInitialize]
        public void Initialize()
        {
            // Initialize AutoMapper configuration
            AutoMapperConfig.Configure();
        }

        [TestMethod]
        public void GetUserById_WithInvalidId_ReturnsNull()
        {
            // Arrange
            int invalidUserId = -1;

            // Act
            var user = _userService.GetUserById(invalidUserId);

            // Assert
            Assert.IsNull(user);
        }


    }
}
