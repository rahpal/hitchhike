// For an introduction to the Page Control template, see the following documentation:
// http://go.microsoft.com/fwlink/?LinkId=232511
(function () {
    "use strict";

    var that = null;

    function planViewModel() {

        that = this;

        that.apiPlan = null;
        that.listView = null;
        that.planDataSource = {
            data: ko.observable(),
            list: ko.observable()
        };
        that.showProgressBar = ko.observable(false);
        that.hidePageContent = ko.observable(true);

        that.deleteListItem = function (eventInfo) {
            if (!!eventInfo) {
                var currentIndex = that.listView.currentItem.index;
                // delete the selected item from the datasource
                // Set the expiration column for the corresponding item
                // Get the item here
                that.listView.itemDataSource.itemFromIndex(currentIndex).done(function (item) {
                    // send travelid to update the record
                    that.showProgressBar(true);
                    that.apiPlan.updateTravelPlan(item.data.PlanId).done(function () {
                        if (true) {
                            that.showProgressBar(true);
                            that.apiPlan.getAllTravelPlanByUser().done(function () {
                                // Update the IListDataSource 
                                that.listView.itemDataSource.remove(that.listView.currentItem.key).done(function () {
                                    // Count the items in the list.
                                    that.listView.itemDataSource.getCount().done(function (count) {
                                        if (count === 0) {
                                            that.hidePageContent(true);
                                        }
                                    });
                                });
                            }).fail(function () {

                            }).always(function () {
                                that.showProgressBar(false);
                            });
                        }
                    }).fail(function () {

                    }).always(function () {
                        that.showProgressBar(false);
                    });
                });
            }
        };

        require(["js/api/plan"], function (apiPlan) {
            that.apiPlan = apiPlan;

            that.showProgressBar(true);
            that.apiPlan.getAllTravelPlanByUser().done(function (result) {

                if (!!result && result.length) {
                    // Store the result set in the data observable
                    that.planDataSource.data(result);
                    //Adding the Click event to each object
                    for (var index = 0; index < result.length; index++) {
                        var travelDate = new Date(result[index].TravelDateTime);
                        var tarvelDateInUTC = new Date(travelDate.toLocaleString() + " UTC");
                        result[index]["TravelDate"] = tarvelDateInUTC.toLocaleDateString();
                        result[index]["TravelTime"] = tarvelDateInUTC.toLocaleTimeString();
                        result[index].deleteListItem = WinJS.Utilities.markSupportedForProcessing(function (evt) {
                            that.deleteListItem(evt);
                        });
                    }

                    // Bind data to Template and Parse the result set
                    that.planDataSource.list(new WinJS.Binding.List(result));

                    that.listView = document.querySelector("#plan-listview").winControl;
                    that.listView.itemDataSource = that.planDataSource.list().dataSource;
                    that.listView.oniteminvoked = that.onListItemClick;

                    WinJS.UI.processAll();

                    that.hidePageContent(false);
                } else {
                    // Handle no data scenario
                    that.hidePageContent(true);
                }
            }).fail(function () {

            }).always(function () {
                that.showProgressBar(false);
            });
        });
    };

    WinJS.UI.Pages.define("/pages/plan/plan.html", {
        // This function is called whenever a user navigates to this page. It
        // populates the page elements with the app's data.
        ready: function (element, options) {
            var self = this,
                appHeader = document.getElementById("app-header");

            ko.applyBindings(new planViewModel(), document.body);

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
            // TODO: Initialize the page here.
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
