using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Principal;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

using Contracts.BusinessContract;
using Utilities;
using Repository;
using SqlFaultTolerantHandler;
using AppAPI.Models;

namespace AppAPI.ActionFilters
{
    public class UserValidationHandler : DelegatingHandler
    {
        /// <summary>
        /// User repository object
        /// </summary>
        private readonly UserRepository userRepository = new UserRepository();

        /// <summary>
        /// Handler SendAsync method
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        protected async override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (request != null)
            {
                if (request.Headers.Contains("Authorization"))
                {
                    if (null == request.Headers.GetValues("Authorization").FirstOrDefault())
                    {
                        return request.CreateErrorResponse(HttpStatusCode.Unauthorized, "Token not found");
                    }

                    var encryptedToken = request.Headers.GetValues("Authorization").First();
                    try
                    {
                        var plainToken = Encryption.Decrypt(encryptedToken, Constants.EncryptionKey);

                        var user = ValidateToken(plainToken);

                        if (null == user && user.UserId == 0)
                        {
                            return request.CreateErrorResponse(HttpStatusCode.Unauthorized, "Invalid Token");
                        }
                        else
                        {
                            // Setting the Claim Principal
                            IList<Claim> claimCollections = new List<Claim>
                            {
                                new Claim(CustomClaimTypes.UserId, user.UserId.ToString()),
                                new Claim(CustomClaimTypes.Username, user.Username),
                                new Claim(CustomClaimTypes.Firstname, user.Firstname),
                                new Claim(CustomClaimTypes.Lastname, user.Lastname),
                                new Claim(CustomClaimTypes.EmailAddress, user.EmailAddress)
                            };

                            var claimIdentity = new ClaimsIdentity(claimCollections, "Basic", user.Username, null);
                            var claimPrincipal = new ClaimsPrincipal(claimIdentity);

                            this.SetPrincipal(claimPrincipal);

                        }
                    }
                    catch (Exception ex)
                    {
                        ExceptionLogger.ProcessException(ex);
                    }
                }
                else
                {
                    return request.CreateErrorResponse(HttpStatusCode.Unauthorized, "Request is missing authorization token");
                }
            }

            return await base.SendAsync(request, cancellationToken);
        }

        /// <summary>
        /// Set thread and HttpContext.User Principal
        /// <param name="principal"> Generic Principal </param>
        /// </summary>
        private void SetPrincipal(IPrincipal principal)
        {
            Thread.CurrentPrincipal = principal;
            if (HttpContext.Current != null)
            {
                HttpContext.Current.User = principal;
            }
        }

        /// <summary>
        /// Validate token
        /// </summary>
        /// <param name="token"></param>
        /// <returns>Return valid user, if not, return null</returns>
        private User ValidateToken(string token)
        {
            string[] stringSeparator = { "$|$" };
            var tokenMembers = token.Split(stringSeparator, StringSplitOptions.None);

            if (tokenMembers.Length > 0)
            {
                // Get all users
                var userList = this.userRepository.GetUsers();

                var currentUser = (from user in userList
                                   where user.UserId == int.Parse(tokenMembers[0]) && user.Username == tokenMembers[1]
                                   select user).FirstOrDefault();

                return currentUser;
            }

            return null;
        }
    }
}