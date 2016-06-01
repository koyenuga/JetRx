using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using JetRx.Entities;
using JetRx.Common.Services;
using JetRx.Api.Filters;
using JetRx.Common.Attributes;

namespace JetRx.Api.Controllers
{
    [JetRxAuthorization(MustHaveDeviceKey =true)]
    public class UserController : BaseController
    {
        

        // GET: api/User/5
        /// <summary>
        /// Authenticate User
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        [ResponseCodes(HttpStatusCode.OK, HttpStatusCode.NotFound, HttpStatusCode.BadRequest, HttpStatusCode.InternalServerError,
          HttpStatusCode.Unauthorized)]
        public response<user> Login ( user user)
        {
            response<user> response = new response<user>();

            try
            {

                response.details = new AuthorizationService().LoginUser(userSession.AppId, userSession.Device.id, user.password, user.email, user.phone_number);
                response.issuccessful = true;
                response.message = "Successful";
            }
            catch (Exception e)
            {
                response.issuccessful = false;
                response.message = e.Message;
            }

            return response;

        }

        /// <summary>
        /// Register New User
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        [ResponseCodes(HttpStatusCode.OK, HttpStatusCode.NotFound, HttpStatusCode.BadRequest, HttpStatusCode.InternalServerError,
        HttpStatusCode.Unauthorized)]
        public response<user> Register(user User)
        {
            response<user> response = new response<user>();

            try
            {
                response.details = new AuthorizationService().RegisterUser(userSession.AppId, userSession.Device.id, User);
                response.issuccessful = true;
                response.message = "Successful";
            }
            catch (Exception e)
            {
              throw e;
            }

            return response;
        }

    }
}
