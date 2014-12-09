define(["js/api/api-base"], function (apibase) {
    var apiPlan = {},
        stringPlan = "Plan";

    apiPlan.getAllTravelPlanByUser = function () {
        ///<summary> Get All created plan for a user </summary>

        return apibase.get(
                    stringPlan + "/GetAllTravelPlanByUser", null, null);
    };

    apiPlan.updateTravelPlan = function (query) {
        ///<summary> Update the record </summary>

        return apibase.get(
                    stringPlan + "/UpdateTravelPlan", null, "?planId=" + query);
    };

    return apiPlan;
});