using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Contracts.BusinessContract;

namespace Repository.Interfaces
{
    public interface ISignupRepository
    {
        /// <summary>
        /// Insert User data into DB
        /// </summary>
        /// <param name="user"></param>
        void CreateUser(Signup user);

        /// <summary>
        /// Check for Username existence
        /// </summary>
        /// <param name="username">Entered username</param>
        /// <returns> true/false </returns>
        bool CheckForUsernameExistence(string username);
    }
}
