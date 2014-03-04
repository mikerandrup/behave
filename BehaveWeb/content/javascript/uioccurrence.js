$(document).ready(function (exports) {

    'use strict';

    var elements = {
        existingList: "#existingList",

        editOccurrenceId: "#ModelInputs input[data-field=occurrenceid]",
        editHabitId: "#ModelInputs input[data-field=habitid]",
        editEventTime: "#ModelInputs input[data-field=eventtime]",
        editNotes: "#ModelInputs input[data-field=notes]",

        status: "#status",

        saveButton: "#save",
        deleteButton: "#delete"
    },

    callbacks = {
        doSave: function (evt) {
            var model = modelInputs.getModel();
            serverRequests.save(model);
        },
        doDelete: function (evt) {
            var id = modelInputs.getModel().OccurrenceId;
            serverRequests.deletion(id);
        },
        doLoad: function (evt) {
            var id = evt.currentTarget.getAttribute("data-occurrence-id");
            serverRequests.loadSingle(id);
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
        save: function (occurrence) {
            var verb = "POST",
                url = "/api/occurrence";

            if (occurrence.OccurrenceId != "") {
                verb = "PUT";
                url += "/" + occurrence.OccurrenceId;
            }

            status.update("Starting " + verb);
            $.ajax({
                url: url,
                method: verb,
                data: occurrence,
                success: function (data) {
                    console.error(arguments);
                    modelInputs.clear();
                    status.update(verb + " Complete.");

                    serverRequests.refreshList();
                }
            });
        },
        deletion: function (id) {
            status.update("Sending Delete for #" + id);
            $.ajax({
                url: "/api/habit/" + id,
                method: "DELETE",
                success: function () {
                    modelInputs.clear();
                    status.update("#" + id + " Deleted.");
                    setTimeout(serverRequests.refreshList, 1000);
                }
            });
        },
        refreshList: function () {
            status.update("Refreshing List...");
            $.ajax({
                url: "/api/occurrence",
                method: "GET",
                success: function (data) {
                    elements.existingList.innerHTML = render.existingList(data);
                    status.update("Fetch Complete.");
                }
            });
        },
        loadSingle: function (id) {
            status.update("Loading #" + id + ".");
            $.ajax({
                url: "/api/occurrence/" + id,
                method: "GET",
                success: function (data) {
                    modelInputs.setFromModel(data);
                    status.update("#" + id + " Loaded Successfully.");
                }
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

    modelInputs = {
        setFromModel: function (model) {
            elements.editOccurrenceId.value = model.OccurrenceId;
            elements.editHabitId.value = model.HabitId;
            elements.editEventTime.value = model.EventTime;
            elements.editNotes.value = model.Notes;
        },
        getModel: function () {
            return {
                OccurrenceId: elements.editOccurrenceId.value,
                HabitId: elements.editHabitId.value,
                EventTime: elements.editEventTime.value,
                Notes: elements.editNotes.value,
            };
        },
        clear: function () {
            elements.editOccurrenceId.value = "";
            elements.editHabitId.value = "";
            elements.editEventTime.value = "";
            elements.editNotes.value = "";
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

        serverRequests.refreshList();

        $(elements.existingList).on("click", "li", callbacks.doLoad);
        $(elements.saveButton).on("click", callbacks.doSave);
        $(elements.deleteButton).on("click", callbacks.doDelete);
    };

    return startupFunction;
}(this));