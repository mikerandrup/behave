﻿$(document).ready(function (exports) {

    'use strict';

    var elements = {
        entryList: "#habitsForDay",
        status: "#status",
        dateControl: "#dateControl",
        userControl: "#userControl"
    },

    callbacks = {
        markDone: function (evt) {
            var el = evt.currentTarget,
                habitId = el.getAttribute("data-habit-id");
            el.setAttribute("data-occurrences", "");
            status.update("Creating new occurrence for this habit.");
            serverRequests.createNewOccurrence(habitId, el);
        },
        markUndone: function (evt) {
            var el = evt.currentTarget,
                habitIdList = el.getAttribute("data-occurrences").split(',');
            el.removeAttribute("data-occurrences");
            status.update("Removing all occurrences for habit.");
            serverRequests.deleteOldOccurrences(habitIdList);
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
        },
        createNewOccurrence: function (habitId, elementNeedingAttribute) {

            var occurrenceData = {
                UserId: elements.userControl.getAttribute("data-user-id"),
                HabitId: habitId,
                EventTime: elements.dateControl.getAttribute("data-date"),
                Notes: "Occurrence created automatically by DailyView web interface click"
            }

            $.ajax({
                url: "/api/occurrence",
                method: "POST",
                data: occurrenceData,
                success: function (createdId) {
                    status.update("Created Occurrence #" + createdId);
                    elementNeedingAttribute.setAttribute("data-occurrences", createdId);
                }
            });
        },
        deleteOldOccurrences: function (occurrenceList) {
            for (var i = 0, l = occurrenceList.length; i < l; i++) {
                $.ajax({
                    url: "/api/occurrence/" + occurrenceList[i],
                    method: "DELETE",
                });
            }
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

    render = {},

    startupFunction = function () {
        serverRequests.init();
        methods.attachElements(elements);

        $(elements.entryList).on("click", "li[data-occurrences]", callbacks.markUndone);
        $(elements.entryList).on("click", "li:not([data-occurrences])", callbacks.markDone);
    };

    return startupFunction;
}(this));