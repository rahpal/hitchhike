using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using Repository;
using Contracts.BusinessContract;
using SqlFaultTolerantHandler;

namespace AppAPI.Controllers
{
    public class HomeController : ApiController
    {
        /// <summary>
        /// Create plan for user
        /// </summary>
        /// <param name="planData"></param>
        [HttpPost]
        [ActionName("CreateTravelPlan")]
        public void CreateTravelPlan(Plan planData)
        {
            try
            {
                // Setting UserId from Current Claims
                var homeRespository = new HomeRepository();
                homeRespository.CreateTravelPlan(planData, UserPrincipal.UserIdentityId);
            }
            catch (Exception ex)
            {
                ExceptionLogger.ProcessException(ex);
            }
        }

        /// <summary>
        /// Get travel plan count by user
        /// </summary>
        [HttpGet]
        [ActionName("GetTravelPlanCountByUser")]
        public int GetTravelPlanCountByUser()
        {
            int planCount = 0;
            try
            {
                // Setting UserId from Current Claims
                var homeRespository = new HomeRepository();
                planCount = homeRespository.GetTravelPlanCountByUser(UserPrincipal.UserIdentityId);
            }
            catch (Exception ex)
            {
                ExceptionLogger.ProcessException(ex);
            }

            return planCount;
        }
    }
}
