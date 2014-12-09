using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Utilities;
using Contracts.BusinessContract;

namespace Repository
{
    public static class UserAccessTokenRepository
    {
        /// <summary>
        /// Generate Access Token
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="username"></param>
        /// <returns></returns>
        private static string GenerateAccessToken(int userId, string username)
        {
            return Encryption.Encrypt(string.Format("{0}$|${1}$|${2}", userId, username, DateTime.Now), Constants.EncryptionKey);
        }

        public static AuthToken CreateUserAccessToken(User userContext)
        {
            var accesstoken = GenerateAccessToken(userContext.UserId, userContext.Username);

            // Create AuthToken object
            var authToken = new AuthToken
            {
                AccessToken = accesstoken,
                IdToken = null,
                Expiry = DateTime.Today.AddDays(1)
            };

            return authToken;
        }

        public static string UpdateUserAccessToken()
        {
            // WLF: Work in Progress
            return null;
        }
    }
}
