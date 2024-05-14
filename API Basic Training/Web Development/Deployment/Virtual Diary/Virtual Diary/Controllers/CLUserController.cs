﻿using System.Net;
using System.Web.Http;
using Virtual_Diary.BasicAuth;
using Virtual_Diary.BL;
using Virtual_Diary.Helper;
using Virtual_Diary.Models;

namespace Virtual_Diary.Controllers
{
    /// <summary>
    /// Controller for managing user operations.
    /// </summary>
    [BasicAuthentication]
    [BasicAuthorisation(Roles = "admin,superadmin")]
    public class CLUserController : ApiController
    {
        public Response response;

        /// <summary>
        /// Creates a new user.
        /// </summary>
        /// <param name="newUser">The user object to be created.</param>
        /// <returns>An HTTP response indicating the success or failure of the operation.</returns>
        [HttpPost]
        [AllowAnonymous]
        public IHttpActionResult CreateUser(User objUser)
        {
            response = new Response();
            BLUser.operation = EnumMessage.I;
            BLUser.PreSave(objUser);

            response = BLUser.Validate();

            if (!response.IsError)
            {
                response = BLUser.Save();
            }
            return Ok(response);
        }

        /// <summary>
        /// Retrieves all users.
        /// </summary>
        /// <returns>A Response object containing all users.</returns>
        [HttpGet]
        public IHttpActionResult GetUsers()
        {
            response = new Response();
            response = BLUser.GetUsers();
            if (response.IsError)
            {
                return Content(HttpStatusCode.InternalServerError, response.Message);
            }
            return Ok(response);
        }

        /// <summary>
        /// Retrieves a user by their ID.
        /// </summary>
        /// <param name="id">The ID of the user to retrieve.</param>
        /// <returns>An HTTP response containing the user object if found, otherwise an error message.</returns>
        [HttpGet]
        public IHttpActionResult GetUserById(int id)
        {
            response = new Response();
            response = BLUser.GetUserById(id);
            if (response.IsError)
            {
                return Content(HttpStatusCode.NotFound, response.Message);
            }
            return Ok(response);
        }

        /// <summary>
        /// Updates an existing user's information.
        /// </summary>
        /// <param name="id">The ID of the user to update.</param>
        /// <param name="updatedUser">The updated user object.</param>
        /// <returns>An HTTP response indicating the success or failure of the operation.</returns>
        [HttpPut]
        public IHttpActionResult UpdateUser(User objUser)
        {
            response = new Response();
            BLUser.operation = EnumMessage.U;
            BLUser.PreSave(objUser);

            response = BLUser.Validate();

            if (!response.IsError)
            {
                response = BLUser.Save();
            }

            return Ok(response);

        }

        /// <summary>
        /// Deletes a user by their ID.
        /// </summary>
        /// <param name="id">The ID of the user to delete.</param>
        /// <returns>An HTTP response indicating the success or failure of the operation.</returns>
        [HttpDelete]
        public IHttpActionResult DeleteUser(int id)
        {
            response = new Response();
            BLUser.operation = EnumMessage.D;
            response = BLUser.ValidateOnDelete(id);
            if (!response.IsError)
            {
                response = BLUser.DeleteUser(id);
            }
            return Ok(response);
        }
    }
}