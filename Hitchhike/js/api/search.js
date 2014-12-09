define(["js/api/api-base"], function (apibase) {
    var apiSearch = {},
        stringSearch = "Search";

    apiSearch.getSearchedTravelPlans = function (query) {
        ///<summary> Get travel plans based on search </summary>

        return apibase.post(
                    stringSearch + "/GetSearchedTravelPlans", null, null, query);
    };

    apiSearch.joinTravelPlan = function(query) {
        ///<summary> Get travel plans based on search </summary>

        return apibase.post(
                    stringSearch + "/FollowTravelPlan", null, null, query);
    };

    return apiSearch;
});