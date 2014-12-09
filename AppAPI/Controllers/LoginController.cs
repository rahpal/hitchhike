using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using Repository;
using Contracts.BusinessContract;
using SqlFaultTolerantHandler;
using System.Text;
using System.Net.Http.Headers;

namespace AppAPI.Controllers
{
    public class LoginController : ApiController
    {
        /// <summary>
        /// Allow Login
        /// </summary>
        /// <param name="model"> Username and password </param>
        /// <returns>HTTP response </returns>
        [HttpPost]
        [ActionName("AppLogin")]
        public HttpResponseMessage AppLogin(Login model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "ModelStateError");
                }

                // Make call to DB to authenticate login user
                var loginRepository = new LoginRepository();
                var userContext = loginRepository.Authenticate(model);

                if (!userContext.IsAuthenticated && userContext.UserId == 0)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.Forbidden, "User is not authenticated");
                }

                // Get token for current logged-in user
                var userWithAuthToken = UserAccessTokenRepository.CreateUserAccessToken(userContext);
                userWithAuthToken.UserContext = userContext;

                // Creating HTTPResponse object 
                HttpResponseMessage response = Request.CreateResponse<AuthToken>(HttpStatusCode.OK, userWithAuthToken);

                return response;
            }
            catch (Exception ex)
            {
                ExceptionLogger.ProcessException(ex);
            }

            return Request.CreateErrorResponse(HttpStatusCode.ServiceUnavailable, "Error occured");
        }
    }
}
