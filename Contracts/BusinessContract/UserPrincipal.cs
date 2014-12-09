using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.BusinessContract
{
    public class UserPrincipal
    {
        #region Fields

        public int UserIdentityId { private set; get; }

        public string Username { private set; get; }

        public string Firstname { private set; get; }

        public string Lastname { private set; get; }

        public string EmailAddress { private set; get; }

        #endregion

        #region Constructor

        public UserPrincipal(
               int userIdentityId,
               string username,
               string firstname,
               string lastname,
               string emailAddress)
        {
            this.UserIdentityId = userIdentityId;
            this.Username = username;
            this.Firstname = firstname;
            this.Lastname = lastname;
            this.EmailAddress = emailAddress;
        }

        #endregion
    }
}
