define(function () {

    function viewModel() {

        var that = this;

        that._setHeader = function () {
            // Setting header text and image
            document.getElementById("header-name").innerText = WinJS.Application.sessionState.User.user.Name;
            document.getElementById("header-emailAddress").innerText = WinJS.Application.sessionState.User.user.EmailAddress;
            document.getElementById("header-gender-icon").className += WinJS.Application.sessionState.User.user.Gender === 1 ?
                                                                        " header-profile-picture-male" : " header-profile-picture-female";

            // Logout and setting profile functionality
            var settingsButton = document.getElementById("header-settings-button");

            // Make setting button clickable with animation
            require(["pages/shared/modules/animation"], function (animation) {
                animation.apply(self, arguments);
                self.addPointerDownHandlers(settingsButton);
                self.addPointerUpHandlers(settingsButton);
            });

            // Initialize event listeners.
            settingsButton.addEventListener("click", this.showSettingsFlyout, false);
            document.getElementById("header-menu-profile").addEventListener("click", this._showProfile, false);
            document.getElementById("header-menu-logout").addEventListener("click", this._logOut, false);
        };

        that.showSettingsFlyout = function (flyoutElement, anchorElement) {
            // Logout and setting profile functionality
            var flyoutElement = document.getElementById("header-flyout-menu"),
                anchorElement = document.getElementById("header-settings-button");

            flyoutElement.winControl.show(anchorElement, "bottom");
        };

        that._showProfile = function () {
            var flyoutElement = document.getElementById("header-flyout-menu");
            flyoutElement.winControl.hide();
        };

        that._logOut = function () {
            var flyoutElement = document.getElementById("header-flyout-menu");
            flyoutElement.winControl.hide();

            // Logout feature
            WinJS.Application.sessionState.User = null;
            WinJS.Navigation.navigate('/pages/login/login.html');
        };

        // Updates the back button state. Called after navigation has
        // completed.
        that._updateBackButton = function () {
            var backButton = document.querySelector("header[role=banner] .win-backbutton");
            if (backButton) {
                backButton.onclick = function () { WinJS.Navigation.back(); };

                if (WinJS.Navigation.canGoBack) {
                    backButton.removeAttribute("disabled");
                } else {
                    backButton.setAttribute("disabled", "disabled");
                }
            }
        }

        return that;
    }

    return viewModel;
});