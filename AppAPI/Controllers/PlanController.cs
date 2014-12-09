using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using Contracts.BusinessContract;
using SqlFaultTolerantHandler;
using Repository;

namespace AppAPI.Controllers
{
    public class PlanController : ApiController
    {
        /// <summary>
        /// Get all travel plan by user
        /// </summary>
        /// <returns> List of Travel plan </returns>
        [HttpGet]
        [ActionName("GetAllTravelPlanByUser")]
        public IEnumerable<Plan> GetAllTravelPlanByUser()
        {
            IEnumerable<Plan> planList = new List<Plan>();
            try
            {
                var planRespository = new PlanRepository();
                planList = planRespository.GetAllTravelPlanByUser(UserPrincipal.UserIdentityId);
            }
            catch (Exception ex)
            {
                ExceptionLogger.ProcessException(ex);
            }

            return planList;
        }

        /// <summary>
        /// Delete travel plan
        /// </summary>
        [HttpGet]
        [ActionName("UpdateTravelPlan")]
        public void UpdateTravelPlan(int planId)
        {
            try
            {
                var planRespository = new PlanRepository();
                planRespository.UpdateTravelPlan(planId);
            }
            catch (Exception ex)
            {
                ExceptionLogger.ProcessException(ex);
            }
        }
    }
}
