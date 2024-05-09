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
using System.Resources;
using System.Runtime.Remoting.Messaging;
using UserManagement.Resources;
using AutoMapper;
using UserManagement.DTOs;

namespace UserManagement.Controllers
{

    [Authorize]
    [RoutePrefix("api")]
    public class UsersController : ApiController
    {

        private readonly UserService _userService;
        private readonly ResourceManager _resourceManager;

        public UsersController()
        {
            _resourceManager = new ResourceManager(typeof(Messages));
            _userService = new UserService();
        }

        // GET api/user
        /// <summary>
        /// Retrieves all users.
        /// </summary>
        [HttpGet]
        [Route("users")]
        public HttpResponseMessage Get(string language = "en")
        {
            try
            {
                var users = _userService.GetAllUsers();
                if (users != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, users);
                }
                else
                {
                    string message = _resourceManager.GetString("UsersNotFound", new System.Globalization.CultureInfo(language));
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, message);
                }
            }
            catch 
            {
                string message = _resourceManager.GetString("InternalServerError", new System.Globalization.CultureInfo(language));
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, message);
            }
        }

        // GET api/user/5
        /// <summary>
        /// Retrieves a user by ID.
        /// </summary>
        [HttpGet]
        [Route("user/{id}")]
        public HttpResponseMessage Get(int id, string language = "en")
        {
            try
            {
                var user = _userService.GetUserById(id);
                if (user != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, user);
                }
                else
                {
                    string message = string.Format(_resourceManager.GetString("UserNotFound", new System.Globalization.CultureInfo(language)), id);
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, message);
                }
            }
            catch
            {
                string message = _resourceManager.GetString("InternalServerError", new System.Globalization.CultureInfo(language));
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, message);
            }

        }

        // POST api/user
        /// <summary>
        /// Creates a new user.
        /// </summary>
        [HttpPost]
        [Route("user")]
        public HttpResponseMessage Post([FromBody] User user, string language = "en")
        {
            try
            {
                _userService.AddUser(user);
                var response = Request.CreateResponse(HttpStatusCode.Created, user);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = user.Id }));
                string message = _resourceManager.GetString("UserCreated");
                response.Content = new StringContent(message);
                return response;
            }
            catch (ArgumentException ex)
            {
                string message = _resourceManager.GetString("BadRequest", new System.Globalization.CultureInfo(language));
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, message, ex);
            }
            catch
            {
                string message = _resourceManager.GetString("InternalServerError", new System.Globalization.CultureInfo(language));
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, message);
            }
        }

        // PUT api/user/5
        /// <summary>
        /// Updates an existing user.
        /// </summary>
        [HttpPut]
        [Route("user/{id}")]
        public HttpResponseMessage Put(int id, [FromBody] User user, string language = "en")
        {
            try
            {
                user.Id = id;
                _userService.UpdateUser(user);
                string message = _resourceManager.GetString("UserUpdated", new System.Globalization.CultureInfo(language));
                return Request.CreateResponse(HttpStatusCode.OK, message);
            }
            catch (ArgumentException ex)
            {
                string message = _resourceManager.GetString("BadRequest", new System.Globalization.CultureInfo(language));
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, message, ex);
            }
            catch
            {
                string message = _resourceManager.GetString("InternalServerError", new System.Globalization.CultureInfo(language));
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, message);
            }
        }

        // DELETE api/user/5
        /// <summary>
        /// Deletes a user by ID.
        /// </summary>
        [HttpDelete]
        [Route("user/{id}")]
        public HttpResponseMessage Delete(int id, string language = "en")
        {
            try
            {
                _userService.DeleteUser(id);
                string message = _resourceManager.GetString("UserDeleted", new System.Globalization.CultureInfo(language));
                return Request.CreateResponse(HttpStatusCode.OK, message);
            }
            catch (ArgumentException ex)
            {
                string message = _resourceManager.GetString("BadRequest", new System.Globalization.CultureInfo(language));
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, message, ex);
            }
            catch
            {
                string message = _resourceManager.GetString("InternalServerError", new System.Globalization.CultureInfo(language));
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, message);
            }
        }

    }
}