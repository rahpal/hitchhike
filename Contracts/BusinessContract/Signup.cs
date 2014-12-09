using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.BusinessContract
{
    public class Signup
    {
        /// <summary>
        /// User name 
        /// </summary>
        public string Username { set; get; }

        /// <summary>
        /// First name
        /// </summary>
        public string FirstName { set; get; }

        /// <summary>
        /// Last Name
        /// </summary>
        public string LastName { set; get; }

        /// <summary>
        /// Gender
        /// </summary>
        public string Gender { set; get; }

        /// <summary>
        /// User Password
        /// </summary>
        public string Password { set; get; }

        /// <summary>
        /// Email Address
        /// </summary>
        public string PrimaryEmailAddress { set; get; }

        /// <summary>
        /// Phone Number
        /// </summary>
        public string ContactNumber { set; get; }
    }
}
