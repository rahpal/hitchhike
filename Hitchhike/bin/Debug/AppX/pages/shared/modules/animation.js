define(function () {

    function viewModel() {

        var that = this;

        that.onPointerDown = function (evt) {
            WinJS.UI.Animation.pointerDown(evt.target);
            evt.preventDefault();
        },

        that.onPointerUp = function (evt) {
            WinJS.UI.Animation.pointerUp(evt.target);
            evt.preventDefault();
        },

        that.addPointerDownHandlers = function (target) {
            target.addEventListener("pointerdown", this.onPointerDown, false);
            target.addEventListener("touchstart", this.onPointerDown, false);
            target.addEventListener("mousedown", this.onPointerDown, false);
        },

        that.addPointerUpHandlers = function (target) {
            target.addEventListener("pointerup", this.onPointerUp, false);
            target.addEventListener("touchend", this.onPointerUp, false);
            target.addEventListener("mouseup", this.onPointerUp, false);
        }

        return that;
    }

    return viewModel;
});