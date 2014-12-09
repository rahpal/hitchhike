using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts.BusinessContract;

namespace Repository.Interfaces
{
    internal interface ISearchRepository
    {
        /// <summary>
        /// All planned travels based on search criteria
        /// </summary>
        /// <param name="searchData">Parameters on which planned travels ned to be searched</param>
        /// <param name="userId"></param>
        /// <returns>Returns all searched results</returns>
        IEnumerable<Search> GetSearchedTravelPlans(Plan searchData, int userId);

        /// <summary>
        /// All planned travels based on search criteria
        /// </summary>
        /// <param name="searchData">Parameters on which planned travels ned to be searched</param>
        /// <returns>Returns all searched results</returns>
        void JoinTravelPlan(Plan searchData);
    }
}
