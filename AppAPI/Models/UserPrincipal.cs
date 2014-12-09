using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AppAPI.Models;

namespace Contracts.BusinessContract
{
    public static class UserPrincipal
    {
        #region Fields

        public static int UserIdentityId
        {
            get
            {
                int userIdentityId = 0;
                int.TryParse(GetClaimValue(CustomClaimTypes.UserId), out userIdentityId);
                return userIdentityId;
            }
        }

        public static string Username
        {
            get
            {
                return GetClaimValue(CustomClaimTypes.Username);
            }
        }

        public static string Firstname
        {
            get
            {
                return GetClaimValue(CustomClaimTypes.Firstname);
            }
        }

        public static string Lastname
        {
            get
            {
                return GetClaimValue(CustomClaimTypes.Lastname);
            }
        }

        public static string EmailAddress
        {
            get
            {
                return GetClaimValue(CustomClaimTypes.EmailAddress);
            }
        }

        #endregion

        #region Method

        /// <summary>
        /// Get claim value for a given type.
        /// </summary>
        /// <param name="claimType">Claim type.</param>
        /// <returns>Claim value</returns>
        private static string GetClaimValue(string claimType)
        {
            if (!string.IsNullOrEmpty(claimType))
            {
                IEnumerable<Claim> claims = GetClaims();
                if (claims.Any(c => c.Type == claimType))
                {
                    return claims.First(c => c.Type == claimType).Value;
                }
            }

            return string.Empty;
        }

        /// <summary>
        /// Get all claims
        /// </summary>
        /// <returns></returns>
        private static List<Claim> GetClaims()
        {
            ClaimsPrincipal claimsIdentity = ClaimsPrincipal.Current;
            return claimsIdentity.Claims.ToList();
        }

        #endregion
    }
}
