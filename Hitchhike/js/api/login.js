define(["js/api/api-base"], function (apibase) {

    var apiLogin = {},
        stringSignup = "Signup", stringLogin = "Login";

    apiLogin.createUser = function (query) {
        ///<summary> Create an active user </summary>
        ///<param name="query"> Pass parameter </param>

        return apibase.post(
                    stringSignup + "/CreateUser", null, null, query);

    };

    apiLogin.checkForUsernameExistence = function (query) {
        ///<summary> Check Username availability </summary>
        ///<param name="query"> Entered username </param>

        return apibase.get(
                    stringSignup + "/CheckForUsernameExistence", null, "?username=" + query);

    };

    apiLogin.Login = function (query) {
        ///<summary> Allow Login to user </summary>
        ///<param name="query"> Username and password </param>

        return apibase.post(
                    stringLogin + "/AppLogin", null, null, query);
    };

    return apiLogin;

});