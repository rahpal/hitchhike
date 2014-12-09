define(function () {

    var api = {};

    api.winXHR = function (httpMethod, webApiMethod, id, query, postData, enableCache) {

        var deferred = $.Deferred(), url;

        url = "http://localhost/HitchHikeAPI" + "/api/" + webApiMethod + (!!id ? "/" + id : "") + (query || "");

        var requestData = {
            type: httpMethod,
            url: url,
            data: !!postData ? JSON.stringify(postData) : null,
            headers: {
                "Content-type": "application/json"
            },
            responseType: 'json'
        };

        // Check the access token
        if (!!WinJS.Application.sessionState.User
            && !!WinJS.Application.sessionState.User.access_token) {
            requestData.headers["Authorization"] = WinJS.Application.sessionState.User.access_token;
        }

        WinJS.xhr(requestData).then(
              function (data) {
                  deferred.resolve(!!data.response ? JSON.parse(data.response) : null);
              },
              function (error) {
                  switch (error.status) {
                      case 401:
                          break;
                      case 500:
                          break;
                  };

                  WinJS.Application.showMessageDialog("Error " + error.status + ": " + error.responseText);
                  deferred.reject(error);
              });

        return deferred.promise();
    };

    api.get = function (method, id, query) {
        // GET: appapi/method/id?query
        return api.winXHR("GET", method, id, query, null);
    };

    api.post = function (method, id, query, postData) {
        // POST: appapi/method/id?query
        return api.winXHR("POST", method, id, query, postData);
    };

    return api;
});