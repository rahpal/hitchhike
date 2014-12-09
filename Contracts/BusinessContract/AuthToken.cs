using System;

namespace Contracts.BusinessContract
{
    public class AuthToken
    {
        /// <summary>
        /// access token
        /// </summary>
        public string AccessToken { set; get; }

        /// <summary>
        /// IdToken
        /// </summary>
        public string IdToken { set; get; }

        /// <summary>
        /// Token expiry datetime
        /// </summary>
        public DateTime Expiry { set; get; }

        /// <summary>
        /// User context when validated true
        /// </summary>
        public User UserContext { set; get; }
    }
}
