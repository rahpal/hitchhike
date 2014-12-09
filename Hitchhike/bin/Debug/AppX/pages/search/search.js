// For an introduction to the Page Control template, see the following documentation:
// http://go.microsoft.com/fwlink/?LinkId=232511
(function () {
    "use strict";

    var that = null;

    function searchViewModel(options) {

        that = this;

        that.apiSearch = null;
        that.listView = null;
        that.planDataSource = {
            data: ko.observableArray(),
            list: ko.observable()
        };
        that.showProgressBar = ko.observable(false);
        that.hidePageContent = ko.observable(true);
        that.followButtonText = ko.observable('Join');

        that.searchForm = {
            from: ko.observable(options.Source).extend({
                required: true
            }),
            to: ko.observable(options.Destination).extend({
                required: true
            }),
            travelDate: ko.observable(options.TravelDateTime).extend({
                required: true
            })//,
            //startTime: ko.observable(),
            //endTime: ko.observable(),
            //vehicleType: {
            //    source: ko.observableArray([
            //        { Id: 1, Name: 'Personal' },
            //        { Id: 2, Name: 'Cab' }
            //    ]),
            //    selectedOption: ko.observable().extend({
            //        required: true
            //    })
            //},
            //capacity: ko.observable().extend({
            //    required: true,
            //    number: true
            //}),
            //fare: ko.observable().extend({
            //    required: true,
            //    number: true
            //})
        };

        that.onExpand = function (data, event) {
            data.IsExpanded(!data.IsExpanded());
        };

        that.submitSearchFormData = function () {
            var validation = ko.validation.group(that.searchForm, { deep: true }),
                searchData = {};

            if (validation().length === 0) {
                searchData = {
                    Source: that.searchForm.from(),
                    Destination: that.searchForm.to(),
                    TravelDateTime: that.searchForm.travelDate()
                };

                that.searchTravelPlan(searchData);
            } else {
                validation.showAllMessages(true);
                WinJS.Application.showMessageDialog('Fields marked with * cannot be left blank');
                searchData = null;
            }

            return searchData;
        };

        that.joinTravelPlan = function (data, event) {

            if (!!data) {
                var query = {
                    PlanId: data.Travel.PlanId,
                    CreatorId: data.User.UserId,
                    FollowerId: WinJS.Application.sessionState.User.user.UserId
                };

                that.apiSearch.joinTravelPlan(query).done(function (result) {
                    data.ButtonText('Joined');
                }).fail(function () {
                    // Handle scenario
                }).always(function () {

                });
            }
        };

        that.searchTravelPlan = function (query) {
            that.showProgressBar(true);
            that.apiSearch.getSearchedTravelPlans(query).done(function (result) {
                that.planDataSource.data.removeAll();

                if (!!result && result.length) {
                    ko.utils.arrayForEach(result, function (record) {
                        record["IsExpanded"] = ko.observable(false);
                        record["IsCreator"] = (WinJS.Application.sessionState.User.user.UserId === record.User.UserId);
                        record["IsFollower"] = record.IsFollowing;
                        record["ButtonText"] = ko.observable('Join');

                        that.planDataSource.data.push(record);
                    });

                    that.hidePageContent(false);
                } else {
                    // Handle no data scenario
                    that.hidePageContent(true);
                }
            }).fail(function () {

            }).always(function () {
                that.showProgressBar(false);
            });
        }

        that.validateAndSearch = function () {
            var validation = ko.validation.group(that.searchForm, { deep: true }),
                planData = {};

            if (validation().length === 0) {

                planData = {
                    Source: that.planFormData.from(),
                    Destination: that.planFormData.to(),
                    TravelDateTime: that.createDateTime(),
                    VehicleType: that.planFormData.vehicleType.selectedOption().Id,
                    Capacity: that.planFormData.capacity(),
                    TotalFare: that.planFormData.fare()
                };
            } else {
                validation.showAllMessages(true);
                planData = null;
            }
        };

        require(["js/api/search"], function (apiSearch) {
            that.apiSearch = apiSearch;

            if (!!options) {
                that.searchTravelPlan(options);
            }
        });
    };

    WinJS.UI.Pages.define("/pages/search/search.html", {
        // This function is called whenever a user navigates to this page. It
        // populates the page elements with the app's data.
        ready: function (element, options) {
            var self = this,
                appHeader = document.getElementById("app-header");

            ko.applyBindings(new searchViewModel(options), document.body);

            require(["pages/shared/modules/header"], function (header) {
                header.apply(self, arguments);

                WinJS.UI.Fragments.renderCopy("/pages/shared/fragments/header.html").done(function (fragment) {
                    appHeader.appendChild(fragment.firstChild);
                    // Load UI Controls after binding
                    WinJS.UI.processAll(appHeader);
                    self._updateBackButton();
                    self._setHeader();
                });
            });

            document.getElementById("search-datepicker").winControl.current = new Date(options.TravelDateTime);
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
