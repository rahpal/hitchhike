using System;
using System.Collections.Generic;
using System.Web.Http;

using Repository;
using Contracts.BusinessContract;
using SqlFaultTolerantHandler;

namespace AppAPI.Controllers
{
    public class SearchController : ApiController
    {
        /// <summary>
        /// Searches travel plans based on passed values
        /// </summary>
        /// <param name="searchData"></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("GetSearchedTravelPlans")]
        public IEnumerable<Search> GetSearchedTravelPlans(Plan searchData)
        {
            IEnumerable<Search> searchTravelPlanList = new List<Search>();
            try
            {
                var searchRespository = new SearchRepository();
                searchTravelPlanList = searchRespository.GetSearchedTravelPlans(searchData, UserPrincipal.UserIdentityId);
            }
            catch (Exception ex)
            {
                ExceptionLogger.ProcessException(ex);
            }

            return searchTravelPlanList;
        }

        /// <summary>
        /// Join travel plan
        /// </summary>
        /// <param name="planData"></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("FollowTravelPlan")]
        public void FollowTravelPlan(Plan planData)
        {
            try
            {
                var searchRespository = new SearchRepository();
                searchRespository.JoinTravelPlan(planData);
            }
            catch (Exception ex)
            {
                ExceptionLogger.ProcessException(ex);
            }
        }
    }
}
