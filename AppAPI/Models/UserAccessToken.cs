using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppAPI.Models
{
    public class UserAccessToken
    {
        /// <summary>
        /// Access Token
        /// </summary>
        public string Token { set; get; }

        /// <summary>
        /// Username
        /// </summary>
        public string Username { set; get; }

        /// <summary>
        /// User password
        /// </summary>
        public string Password { set; get; }
    }
}