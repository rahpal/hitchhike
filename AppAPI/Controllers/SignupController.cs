using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Threading;
using System.Security.Principal;

using Repository;
using Contracts.BusinessContract;
using SqlFaultTolerantHandler;

namespace AppAPI.Controllers
{
    /// <summary>
    /// The Sign Up Controller/
    /// </summary>
    public class SignupController : ApiController
    {
        /// <summary>
        /// Create an active user. 
        /// </summary>
        /// <param name="user"> User Details </param>
        [HttpPost]
        [ActionName("CreateUser")]
        public void CreateUser(Signup user)
        {
            try
            {
                var signupRepository = new SignupRepository();
                signupRepository.CreateUser(user);
            }
            catch (Exception ex)
            {
                // Log in Instrumentation DB
                ExceptionLogger.ProcessException(ex);
            }
        }

        /// <summary>
        /// Check for username existence
        /// </summary>
        /// <param name="username"> Entered Username </param>
        /// <returns> True/ false </returns>
        [HttpGet]
        [ActionName("CheckForUsernameExistence")]
        public bool CheckForUsernameExistence(string username)
        {
            bool usernameExists = false;
            try
            {
                var signupRepository = new SignupRepository();
                usernameExists = signupRepository.CheckForUsernameExistence(username);
            }
            catch (Exception ex)
            {
                // Log in Instrumentation DB
                ExceptionLogger.ProcessException(ex);
            }

            return usernameExists;

        }

        /// <summary>
        /// Sample
        /// </summary>
        [HttpGet]
        [ActionName("Sample")]
        public string Sample()
        {
            try
            {
            }
            catch (Exception ex)
            {
                // Log in Instrumentation DB
                ExceptionLogger.ProcessException(ex);
            }

            return "Working!!!";
        }
    }
}
