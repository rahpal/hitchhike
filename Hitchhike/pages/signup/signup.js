// For an introduction to the Page Control template, see the following documentation:
// http://go.microsoft.com/fwlink/?LinkId=232511
(function () {
    "use strict";

    function signupViewModel() {
        var that = this;

        that.apiLogin = null;
        that.showAvailProgress = ko.observable(false);
        that.showSubmitProgress = ko.observable(false);
        that.usernameAcquired = ko.observable(false);
        that.passwordValidationText = ko.observable('');
        that.matchPassword = ko.observable(false);

        that.signupFormData = {
            username: ko.observable().extend({
                required: true,
                rateLimit: { timeout: 500, method: "notifyWhenChangesStop" },
                minLength: 4
            }),
            firstname: ko.observable().extend({ required: true }),
            lastname: ko.observable().extend({ required: true }),
            gender: ko.observable('M').extend({ required: true }),  // Default is Male
            password: ko.observable().extend({
                required: true,
                minLength: 4
            }),
            confirmPassword: ko.observable().extend({
                required: true,
                rateLimit: { timeout: 500, method: "notifyWhenChangesStop" }
            }),
            emailAddress: ko.observable().extend({
                required: true,
                email: true
            }),
            contactNumber: ko.observable().extend({ number: true })
        };

        that.signupFormData.username.subscribe(function (enteredUsername) {
            // Reset the username availability message
            that.usernameAcquired(false);
            if (!!enteredUsername && that.signupFormData.username.isValid()) {
                // Make xhr call to check the existence of username
                // If username doesn't exist, create user else ask user to enter different username.
                that.showAvailProgress(true);
                that.apiLogin.checkForUsernameExistence(enteredUsername).done(function (result) {
                    that.usernameAcquired(result);
                }).fail(function (error) {
                    // Handle Error scenario
                }).always(function () {
                    that.showAvailProgress(false);
                });
            }
        }, that);

        that.matchPassword = ko.computed({
            read: function () {
                return ((this.password() !== this.confirmPassword())
                    && !!this.confirmPassword());
            },
            owner: that.signupFormData,
            deferEvaluation: true
        });

        that.readFormData = function () {

            var validation = ko.validation.group(that.signupFormData, { deep: true }),
                userData = {};

            if (validation().length === 0) {

                userData = {
                    Username: that.signupFormData.username(),
                    FirstName: that.signupFormData.firstname(),
                    LastName: that.signupFormData.lastname(),
                    Gender: that.signupFormData.gender(),
                    Password: that.signupFormData.password(),
                    PrimaryEmailAddress: that.signupFormData.emailAddress(),
                    ContactNumber: !!that.signupFormData.contactNumber() ? that.signupFormData.contactNumber() : null
                };

            } else {
                validation.showAllMessages(true);
                userData = null;
            }

            return userData;
        };

        that.submitFormData = function () {

            // Get Sign up form Data
            var userData = that.readFormData();

            if (!!userData && !that.usernameAcquired() && !that.matchPassword()) {
                that.showSubmitProgress(true);
                that.apiLogin.createUser(userData).done(function (result) {
                    WinJS.Application.showMessageDialog('User created');
                    WinJS.Navigation.navigate("/pages/login/login.html", userData.Username);
                }).fail(function () {
                    // Handle Error scenario
                }).always(function () {
                    that.showSubmitProgress(false);
                });
            }
        };

        require(["js/api/login"], function (apiLogin) {
            that.apiLogin = apiLogin;
        });

    };

    WinJS.UI.Pages.define("/pages/signup/signup.html", {
        // This function is called whenever a user navigates to this page. It
        // populates the page elements with the app's data.
        ready: function (element, options) {
            // TODO: Initialize the page here.
            ko.applyBindings(new signupViewModel(), document.body);
        },

        unload: function () {
            // TODO: Respond to navigations away from this page.
            ko.cleanNode(document.body);
        },

        updateLayout: function (element, viewState, lastViewState) {
            /// <param name="element" domElement="true" />
            // TODO: Respond to changes in viewState.
        }
    });
})();
