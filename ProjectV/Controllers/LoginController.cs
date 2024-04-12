using ProjectV.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ProjectV.Controllers
{
    public class LoginController : ApiController
    {
        /// <summary>
        /// POST: /api/LoginUser
        /// API to Login user on the MVC and set session information to be used in later requests
        /// </summary>
        /// <param name="user"></param>
        [HttpPost]
        public IHttpActionResult LoginUser(User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if(UserSecurity.Login(user.Username, user.Password))
            {
                return Ok();
            }
            else
            {
                return Content(HttpStatusCode.Unauthorized, "Invalid Credentials");
            }
        }
    }
}
