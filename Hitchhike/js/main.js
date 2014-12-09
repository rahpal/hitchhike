require.config({
    baseUrl: "",
    waitSeconds: 15,
    paths: {
        "jquery": ["https://ajax.aspnetcdn.com/ajax/jQuery/jquery-2.1.1", "../Scripts/jquery-2.1.1.js"],
        "jqueryui": [],
        "knockout": ["https://ajax.aspnetcdn.com/ajax/knockout/knockout-3.1.0", "../Scripts/knockout-3.1.0.js"]
    }
});

define("knockout", window.ko);

define("main", [
    "jquery",
    "knockout"],
    function (
        $,
        ko) {

        ko.validation.configure({
            messagesOnModified: true,
            insertMessages: false
        });

    });

