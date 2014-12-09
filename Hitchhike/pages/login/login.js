// For an introduction to the Page Control template, see the following documentation:
// http://go.microsoft.com/fwlink/?LinkId=232511
(function () {
    "use strict";

    //  var auth0 = new Auth0Client(
    //"devrahul.auth0.com",
    //"Enoa2zzPAhc9DwKAZuqafz7xP8ChHiYI");

    var applicationData = Windows.Storage.ApplicationData.current,
        roamingSettings = applicationData.roamingSettings;

    function loginViewModel(options) {
        var that = this,
            nav = WinJS.Navigation,
            app = WinJS.Application;

        that.apiLogin = null;
        that.showLoginProgress = ko.observable(false);
        that.isChecked = ko.observable(false);

        that.loginModel = {
            username: ko.observable(!!roamingSettings.values["Username"] ? roamingSettings.values["Username"] : '').extend({ required: true }),
            password: ko.observable(!!roamingSettings.values["Password"] ? roamingSettings.values["Password"] : '').extend({ required: true })
        };

        if (!!options) {
            that.loginModel.username(options);
        }

        that.navigateToSignup = function () {
            nav.navigate("/pages/signup/signup.html");
        };

        that.submitLoginCredentials = function () {
            ///<summary> Submit LOgin Credentials </summary>

            if (that.isChecked()) {
                // Store the username and password in RoamingSetting
                roamingSettings.values["Username"] = that.loginModel.username();
                roamingSettings.values["Password"] = that.loginModel.password();
            }
            // read credentials
            var userCredentials = {},
                validation = ko.validation.group(that.loginModel, { deep: true });

            if (validation().length === 0) {
                userCredentials = Object.freeze({
                    Username: that.loginModel.username(),
                    Password: that.loginModel.password()
                });
            } else {
                validation.showAllMessages(true);
                return;
            }

            if (!$.isEmptyObject(userCredentials)) {
                that.showLoginProgress(true);
                that.apiLogin.Login(userCredentials).done(function (result) {
                    // Store user token and details in sessionState 
                    app.sessionState["User"] = Object.freeze({
                        access_token: result.AccessToken,
                        user: result.UserContext
                    });

                    // Navigate to Homepage
                    nav.navigate("/pages/home/home.html");
                }).fail(function () {
                    app.showMessageDialog('Login Unsuccessful');
                }).always(function () {
                    that.showLoginProgress(false);
                });
            };

            //auth0.Login(function (err, result) {
            //    if (err) return err;
            //    /* 
            //    Use result to do wonderful things, e.g.: 
            //      - get user email => result.Profile.email
            //      - get facebook/google/twitter/etc access token => result.Profile.identities[0].access_token
            //      - get Windows Azure AD groups => result.Profile.groups
            //      - etc.
            //    */
            //});
        };

        require(["js/api/login"], function (apiLogin) {
            that.apiLogin = apiLogin;
        });
    }

    WinJS.UI.Pages.define("/pages/login/login.html", {
        // This function is called whenever a user navigates to this page. It
        // populates the page elements with the app's data.
        ready: function (element, options) {
            // TODO: Initialize the page here.
            ko.applyBindings(new loginViewModel(options), document.body);
        },

        unload: function () {
            // TODO: Respond to navigations away from this page.
            ko.cleanNode(document.body);
        }
    });

})();
