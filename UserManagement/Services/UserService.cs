using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UserManagement.Data;
using UserManagement.Data.Models;
using UserManagement.Data.Repositories;
using UserManagement;
using UserManagement.DTOs;
using AutoMapper;

namespace UserManagement.Services
{
    public class UserService
    {

        private readonly IUserRepository _userRepository;

        public UserService()
        {
            _userRepository = new UserRepository();
        }


        /// <summary>
        /// Retrieves all users from the repository except password data.
        /// </summary>
        /// <returns>A list of all users.</returns>
        public  List<UserDTO> GetAllUsers()
        {

            var users = _userRepository.GetAll();
            return Mapper.Map<ICollection<User>, ICollection<UserDTO>>(users).ToList();
        }

        /// <summary>
        /// Retrieves a user by their ID from the repository.
        /// </summary>
        /// <param name="userId">The ID of the user to retrieve.</param>
        /// <returns>The user with the specified ID.</returns>
        public  UserDTO GetUserById(int userId)
        {
            var user = _userRepository.GetById(userId);
            return Mapper.Map<User, UserDTO>(user);

        }

        /// <summary>
        /// Adds a new user to the repository.
        /// </summary>
        /// <param name="user">The user to add.</param>
        public void AddUser(User user)
        {
            _userRepository.Add(user);
        }

        /// <summary>
        /// Updates an existing user in the repository.
        /// </summary>
        /// <param name="user">The updated user information.</param>
        public void UpdateUser(User user)
        {
            _userRepository.Update(user);
        }

        /// <summary>
        /// Deletes a user from the repository by their ID.
        /// </summary>
        /// <param name="userId">The ID of the user to delete.</param>
        public void DeleteUser(int userId)
        {
            _userRepository.Delete(userId);
        }



    }


}