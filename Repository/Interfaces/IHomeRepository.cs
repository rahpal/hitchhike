using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Contracts.BusinessContract;

namespace Repository.Interfaces
{
    internal interface IHomeRepository
    {
        /// <summary>
        /// Create plan for user
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="planData"></param>
        void CreateTravelPlan(Plan planData, int userId);

        /// <summary>
        /// Get travel plan count
        /// </summary>
        /// <param name="userId"></param>
        int GetTravelPlanCountByUser(int userId);
    }
}
