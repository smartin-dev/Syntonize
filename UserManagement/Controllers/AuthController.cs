using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using UserManagement.Data.Models;
using UserManagement.Data.Repositories;
using UserManagement.DTOs;
using UserManagement.Services;

namespace UserManagement.Controllers
{
    public class AuthController : ApiController
    {

        private readonly AuthService _authService;
        private readonly UserService _userService;

        public AuthController()
        {
            _authService = new AuthService();
            _userService = new UserService();
        }


        // POST api/auth/login
        [HttpPost]
        [Route("api/auth/login")]
        public HttpResponseMessage Login([FromBody]Login login)
        {
            try
            {
                // Authenticate the user using their credentials
                var user = _authService.Authenticate(login.Email, login.Password);

                if (user != null)
                {
                    // Generate JWT token
                    //var token = AuthService.GenerateJwtToken(newUser);
                    var token = TokenGenerator.GenerateTokenJwt(login.Email);

                    // Return JWT token to the client
                    return Request.CreateResponse(HttpStatusCode.OK, new { Token = token });
                }
                else
                {
                    // User authentication failed
                    return Request.CreateResponse(HttpStatusCode.Unauthorized, "Invalid email or password");
                }
            }
            catch (Exception ex)
            {
                // Log or handle the exception
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // POST api/auth/signup
        [HttpPost]
        [Route("api/auth/signup")]
        public HttpResponseMessage SignUp([FromBody] SignUp signup)
        {
            try
            {
                // Check if the email is already registered
                if (_authService.IsEmailRegistered(signup.Email))
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "Email already registered");
                }

                // Hash the password
                byte[] salt;
                var hashedPassword = PasswordHasher.HashPassword(signup.Password, out salt);

                // Create new user with hashed password and salt
                var newUser = new User
                {
                    Name = signup.Name,
                    Email = signup.Email,
                    PasswordHash = hashedPassword,
                    Salt = salt
                };


                // Add the new user to the database
                _userService.AddUser(newUser);

                // Generate JWT token and return it

                //var token = AuthService.GenerateJwtToken(newUser);
                var token = TokenGenerator.GenerateTokenJwt(signup.Email);

                // Return JWT token to the client
                return Request.CreateResponse(HttpStatusCode.OK, new { Token = token });
            }
            catch (Exception ex)
            {
                // Log or handle the exception
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
