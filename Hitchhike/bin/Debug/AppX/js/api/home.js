define(["js/api/api-base"], function (apibase) {
    var apiHome = {},
        stringHome = "Home";

    apiHome.createTravelPlan = function (query) {
        ///<summary> plan created </summary>
        ///<param name="query"> Pass parameter </param>

        return apibase.post(
                    stringHome + "/CreateTravelPlan", null, null, query);
    };

    apiHome.getTravelPlanCountByUser = function () {
        ///<summary> plan created </summary>

        return apibase.get(
                    stringHome + "/GetTravelPlanCountByUser", null, null);
    };

    return apiHome;
});