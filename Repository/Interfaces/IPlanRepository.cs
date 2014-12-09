using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Contracts.BusinessContract;

namespace Repository.Interfaces
{
    internal interface IPlanRepository
    {
        /// <summary>
        /// Get all travel plan by user
        /// </summary>
        /// <returns> List of Travel plan </returns>
        IEnumerable<Plan> GetAllTravelPlanByUser(int userId);

        /// <summary>
        /// Delete the record 
        /// </summary>
        void UpdateTravelPlan(int planId);
    }
}
