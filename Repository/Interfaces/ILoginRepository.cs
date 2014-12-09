using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Contracts.BusinessContract;

namespace Repository.Interfaces
{
    internal interface ILoginRepository
    {
        /// <summary>
        /// Authenticate user
        /// </summary>
        /// <param name="model"></param>
        /// <returns> User context</returns>
        User Authenticate(Login model);
    }
}
