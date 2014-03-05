$(document).ready(function (exports) {

    'use strict';

    var elements = {
        entryList: "#entries",

        status: "#status"
    },

    callbacks = {
        toggleDone: function (evt) {
            var el = evt.currentTarget;

            status.update("Doing pointless toggle.");
            $(el).toggleClass("done");

        }
    },

    serverRequests = {
        init: function () {
            $.ajaxSetup({
                cache: false,
                accepts: "application/json",
                async: true,
                complete: methods.reportResponse
            });
        }
    },

    status = {
        update: function (message) {
            clearTimeout(status.timeoutHandle);
            elements.status.innerHTML = message;

            if (message != "") {
                status.timeoutHandle = setTimeout(
                    function () { status.update(""); },
                    2500
                );
            }
        },
        timeoutHandle: null
    },

    methods = {
        reportResponse: function () {
            console.warn(arguments);
        },
        attachElements: function (elementsObject) {
            for (var key in elementsObject) {
                elementsObject[key] = document.querySelector(elementsObject[key]);
            }
            console.log(elementsObject);
        }
    },

    render = {
        existingList: function (listData) {
            var markup = "",
                entry;

            for (var i = 0, len = listData.Occurrences.length; i < len; i++) {
                entry = listData.Occurrences[i];

                markup += "<li data-occurrence-id=\""
                    + entry.OccurrenceId
                    + "\">"
                    + entry.OccurrenceId
                    + " @ "
                    + entry.EventTime + "</li>";
            }

            return markup;
        }
    },

    startupFunction = function () {
        serverRequests.init();
        methods.attachElements(elements);

        //serverRequests.refreshList();

        $(elements.entryList).on("click", "li", callbacks.toggleDone);
    };

    return startupFunction;
}(this));