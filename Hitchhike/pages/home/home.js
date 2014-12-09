(function () {
    "use strict";

    var pageCompose = null,
        that;

    function homeViewModel() {

        that = this;

        that.apiHome = null;
        that.currentHubId = null;
        that.showProgressForUserTravelPlan = ko.observable();
        that.activeNotificationTab = ko.observable(1);

        that.getTravelPlanData = function (tileContext) {

            tileContext.showTileLoader(true);
            that.apiHome.getTravelPlanCountByUser().done(function (recordCount) {
                if (recordCount >= 0) {
                    tileContext.hub.contents[0].value(recordCount);
                } else {
                    // handle no data scenario
                }
            }).fail(function () {

            }).always(function () {
                tileContext.showTileLoader(false);
            });
        };

        that.homeTiles = [
            {
                hub: {
                    id: 'home-hub-plan-travel-details',
                    title: 'Planned travel',
                    contents: [
                        {
                            text: 'Planned travel',
                            value: ko.observable()
                        }
                    ],
                },
                dataSource: that.getTravelPlanData,
                icon: '../../images/thumbs_up36x36.png',
                uri: '/pages/plan/plan.html',
                hubId: 1,
                showTileLoader: ko.observable(true)
            }
        ];

        that.onTileClick = function (data, event) {
            WinJS.Navigation.navigate(data.uri);
        };

        that.planFormData = {
            from: ko.observable().extend({
                required: true
            }),
            to: ko.observable().extend({
                required: true
            }),
            travelDate: ko.observable().extend({
                required: true
            }),
            travelTime: ko.observable().extend({
                required: true
            }),
            vehicleType: {
                source: ko.observableArray([
                    { Id: 1, Name: 'Personal' },
                    { Id: 2, Name: 'Cab' }
                ]),
                selectedOption: ko.observable().extend({
                    required: true
                })
            },
            capacity: ko.observable().extend({
                required: true,
                number: true
            }),
            fare: ko.observable().extend({
                required: true,
                number: true
            }),
            description: ko.observable()
        };

        that.searchFormData = {
            from: ko.observable().extend({
                required: true
            }),
            to: ko.observable().extend({
                required: true
            }),
            travelDate: ko.observable().extend({
                required: true
            })
        };


        that.selectedDate = function () {
            that.planFormData.travelDate(that.planDatePickerControl._currentDate);
        };

        that.selectedDateForSearch = function () {
            that.searchFormData.travelDate(that.searchDatePickerControl._currentDate);
        };

        that.selectedTime = function () {
            that.planFormData.travelTime(that.planTimePickerControl._currentTime);
        };

        that.createDateTime = function () {
            var selectedDate = that.planFormData.travelDate(),
                selectedTime = that.planFormData.travelTime();

            return new Date(selectedDate.getFullYear(),
                selectedDate.getMonth(),
                selectedDate.getDate(),
                selectedTime.getHours(),
                selectedTime.getMinutes(),
                selectedTime.getSeconds());
        };

        that.readPlanFormData = function () {

            var validation = ko.validation.group(that.planFormData, { deep: true }),
                planData = {};

            if (validation().length === 0) {

                planData = {
                    Source: that.planFormData.from(),
                    Destination: that.planFormData.to(),
                    TravelDateTime: that.createDateTime(),
                    VehicleType: that.planFormData.vehicleType.selectedOption().Id,
                    //Availability: that.planFormData.availability(),
                    Capacity: that.planFormData.capacity(),
                    TotalFare: that.planFormData.fare(),
                    Description: that.planFormData.description()
                };

            } else {
                validation.showAllMessages(true);
                WinJS.Application.showMessageDialog('Fields marked with * cannot be left blank');
                planData = null;
            }

            return planData;
        };

        that.submitPlanFormData = function (data, evt) {
            ///<summary> current context data </summary>
            ///<summary> event </summary>

            var planData = that.readPlanFormData();

            if (!!planData) {
                // Send data to backend
                that.apiHome.createTravelPlan(planData).done(function () {
                    that.getTravelPlanData(that.homeTiles[0]);
                    WinJS.Application.showMessageDialog('Plan Created');
                }).fail(function () {

                });
            }
        };

        that.readSearchFormData = function () {

            var validation = ko.validation.group(that.searchFormData, { deep: true }),
                searchData = {};

            if (validation().length === 0) {

                searchData = {
                    Source: that.searchFormData.from(),
                    Destination: that.searchFormData.to(),
                    TravelDateTime: that.searchFormData.travelDate()
                };

            } else {
                validation.showAllMessages(true);
                WinJS.Application.showMessageDialog('Fields marked with * cannot be left blank');
                searchData = null;
            }

            return searchData;
        };

        that.submitSearchFormData = function () {
            var searchData = that.readSearchFormData();

            if (!!searchData) {
                WinJS.Navigation.navigate("/pages/search/search.html", searchData);
            }
        };

        that.getPlannedNotifications = function (tabId) {
            if (tabId !== that.activeNotificationTab()) {
                that.activeNotificationTab(tabId);

                // TODO: Write code to fetch notifications for planned trips
            }
        };

        that.getSearchedNotifications = function (tabId) {
            if (tabId !== that.activeNotificationTab()) {
                that.activeNotificationTab(tabId);

                // TODO: Write code to fetch notifications for searched trips
            }
        }

        require(["js/api/home"], function (apiHome) {
            that.apiHome = apiHome;

            // Fetch data fpr all tiles
            that.homeTiles.forEach(function (tile, index) {
                if (!!tile && typeof (tile.dataSource) === 'function') {
                    tile.dataSource(tile);
                }
            });
        });

    };

    WinJS.UI.Pages.define("/pages/home/home.html", {
        // This function is called whenever a user navigates to this page. It
        // populates the page elements with the app's data.
        ready: function (element, options) {
            // TODO: Initialize the page here.
            var self = this,
                appHeader = document.getElementById("app-header");

            ko.applyBindings(new homeViewModel(), document.body);

            require(["pages/shared/modules/header", "pages/shared/modules/animation"], function (header, animation) {
                header.apply(self, arguments);
                animation.apply(self, arguments);

                WinJS.UI.Fragments.renderCopy("/pages/shared/fragments/header.html").done(function (fragment) {
                    appHeader.appendChild(fragment.firstChild);
                    // Load UI Controls after binding
                    WinJS.UI.processAll(appHeader);
                    self._updateBackButton();
                    self._setHeader();

                    var hub = document.getElementsByClassName("tile");
                    self.addPointerDownHandlers(hub[0]);
                    self.addPointerUpHandlers(hub[0]);
                });
            });

            that.planDatePickerControl = element.querySelector("#plan-datepicker").winControl;
            that.planDatePickerControl.addEventListener("change", that.selectedDate, false);

            that.planTimePickerControl = element.querySelector("#plan-timepicker").winControl;
            that.planTimePickerControl.addEventListener("change", that.selectedTime, false);

            that.searchDatePickerControl = element.querySelector("#search-datepicker").winControl;
            that.searchDatePickerControl.addEventListener("change", that.selectedDateForSearch, false);
        },

        unload: function () {
            // TODO: Respond to navigations away from this page.
            ko.cleanNode(document.body);
        }
    });
})();
