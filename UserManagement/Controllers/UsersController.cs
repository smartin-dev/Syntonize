using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Web;
using System.Web.Http;
using UserManagement.Data.Models;
using UserManagement.Services;
using UserManagement.Data.Repositories;

namespace UserManagement.Controllers
{

    [Authorize]
    public class UsersController : ApiController
    {

        private readonly IUserRepository _userRepository;

        public UsersController()
        {
            _userRepository = new UserRepository();
        }

        // GET api/user
        /// <summary>
        /// Retrieves all users.
        /// </summary>
        [HttpGet]
        [Route("api/users")]
        public HttpResponseMessage Get()
        {
            var users = _userRepository.GetAll();
            if (users != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, users);
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, $"Users not found.");
            }
        }

        // GET api/user/5
        /// <summary>
        /// Retrieves a user by ID.
        /// </summary>
        [HttpGet]
        [Route("api/user/{id}")]
        public HttpResponseMessage Get(int id)
        {
            var user = _userRepository.GetById(id);
            if (user != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, user);
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, $"User with id {id} not found.");
            }
        }

        // POST api/user
        /// <summary>
        /// Creates a new user.
        /// </summary>
        [HttpPost]
        [Route("api/user")]
        public HttpResponseMessage Post([FromBody] User user)
        {
            try
            {
                _userRepository.Add(user);
                var response = Request.CreateResponse(HttpStatusCode.Created, user);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = user.Id }));
                return response;
            }
            catch (ArgumentException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        // PUT api/user/5
        /// <summary>
        /// Updates an existing user.
        /// </summary>
        [HttpPut]
        [Route("api/user/{id}")]
        public HttpResponseMessage Put(int id, [FromBody] User user)
        {
            try
            {
                user.Id = id; // Ensure that the user ID in the body matches the ID in the URL
                _userRepository.Update(user);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (ArgumentException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        // DELETE api/user/5
        /// <summary>
        /// Deletes a user by ID.
        /// </summary>
        [HttpDelete]
        [Route("api/user/{id}")]
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                _userRepository.Delete(id);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (ArgumentException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex.Message);
            }
        }

    }
}