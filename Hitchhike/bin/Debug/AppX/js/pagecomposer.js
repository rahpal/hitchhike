(function () {
    "use strict";

    WinJS.Namespace.define("Application", {
        PageComposer: WinJS.Class.define(
            // Define the constructor function for the PageControlNavigator.
            function PageComposer(element, options) {
                this._setElement(element);
                this._element.appendChild(this._createPageElement());
                WinJS.UI.setOptions(this, options);
            }, {
                // Private functions, properties
                _element: "",
                _options: null,
                _setElement: function (element) {
                    this._element = element;
                },
                // This is the root element of the current page.
                _pageElement: function () {
                    return this._element.firstElementChild;
                },
                _createPageElement: function () {
                    var element = document.createElement("div");
                    element.setAttribute("dir", window.getComputedStyle(this._element, null).direction);
                    element.style.width = "100%";
                    element.style.height = "100%";
                    return element;
                },
                composer: function (args) {
                    var that = this;
                    var newElement = this._createPageElement();
                    WinJS.UI.Pages.render(args.uri, newElement).done(function () {
                        var oldElement = that._pageElement();
                        that._element.removeChild(oldElement);
                        that._element.appendChild(newElement);
                        oldElement.innerText = "";
                        WinJS.UI.Animation.enterPage(that._pageElement(), null);
                    }, function (err) {
                        // Fallback view
                        var fallbackView = document.createElement("<div>Unable to compose the page.</div>");
                        that._element.removeChild(that._pageElement());
                        that._element.appendChild(fallbackView);
                    });
                }
            }
        )
    });
})();