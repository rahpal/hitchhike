using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppAPI.Models
{
    public static class CustomClaimTypes
    {
        /// <summary>
        /// User Id
        /// </summary>
        public const string UserId = "http://schemas.xmlsoap.org/claims/UserId";

        /// <summary>
        /// Username 
        /// </summary>
        public const string Username = "http://schemas.xmlsoap.org/claims/Username";

        /// <summary>
        /// Firstname
        /// </summary>
        public const string Firstname = "http://schemas.xmlsoap.org/claims/FName";

        /// <summary>
        /// Lastname
        /// </summary>
        public const string Lastname = "http://schemas.xmlsoap.org/claims/LName";

        /// <summary>
        /// EmailAddress
        /// </summary>
        public const string EmailAddress = "http://schemas.xmlsoap.org/claims/EmailAddress";
    }
}