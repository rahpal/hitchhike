using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Contracts.BusinessContract;

namespace Repository.Interfaces
{
    public interface IUserRepository
    {
        /// <summary>
        /// Get list of all users
        /// </summary>
        /// <returns> List of users </returns>
        IEnumerable<User> GetUsers();
    }
}
