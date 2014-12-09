using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.BusinessContract
{
    public class User
    {
        /// <summary>
        /// User IdentityID
        /// </summary>
        public int UserId { set; get; }

        /// <summary>
        /// Username
        /// </summary>
        public string Username { set; get; }

        /// <summary>
        /// FirstName
        /// </summary>
        public string Firstname { set; get; }

        /// <summary>
        /// LastName
        /// </summary>
        public string Lastname { set; get; }

        /// <summary>
        /// Full Name
        /// </summary>
        public string Name
        {
            get
            {
                return string.Format("{0} {1}", this.Firstname, this.Lastname);
            }
        }

        /// <summary>
        /// Gender
        /// </summary>
        public Enum Gender { set; get; }

        /// <summary>
        /// EmailAddress
        /// </summary>
        public string EmailAddress { set; get; }

        /// <summary>
        /// user authentication flag
        /// </summary>
        public bool IsAuthenticated { set; get; }
    }
}
