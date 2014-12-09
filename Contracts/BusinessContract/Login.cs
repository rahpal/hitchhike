using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.BusinessContract
{
    public class Login
    {
        /// <summary>
        /// user name
        /// </summary>
        public string Username { set; get; }

        /// <summary>
        /// password
        /// </summary>
        public string Password { set; get; }
    }
}
