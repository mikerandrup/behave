$(document).ready(function (exports) {

    'use strict';

    var elements = {
        habitList: "#existingHabitsList",

        editHabitId: "#HabitInputs input[data-field=habitid]",
        editUserId: "#HabitInputs input[data-field=userid]",
        editImportance: "#HabitInputs input[data-field=importance]",
        editTitle: "#HabitInputs input[data-field=title]",
        editDetails: "#HabitInputs input[data-field=details]",

        status: "#status",

        saveButton: "#save",
        deleteButton: "#delete"
    },

    callbacks = {
        doSave: function (evt) {
            var habit = habitInputs.getModel();
            serverRequests.save(habit);
        },
        doDelete: function (evt) {
            var id = habitInputs.getModel().HabitId;
            serverRequests.deletion(id);
        },
        doLoad: function (evt) {
            var habitId = evt.currentTarget.getAttribute("data-habit-id");
            serverRequests.loadSingle(habitId);
        }
    },

    serverRequests = {
        init: function () {
            // jquery ajax setup here
            $.ajaxSetup({
                cache: false,
                accepts: "application/json",
                async: true,
                complete: methods.reportResponse
            });
        },
        save: function (habit) {
            var verb = "POST",
                url = "/api/habit";

            if (habit.HabitId != "") {
                verb = "PUT";
                url += "/" + habit.HabitId;
            }

            status.update("Starting " + verb);
            $.ajax({
                url: url,
                method: verb,
                data: habit,
                success: function (data) {
                    console.error(arguments);
                    habitInputs.clear();
                    status.update(verb + " Complete.");

                    setTimeout(serverRequests.refreshList, 1000);
                }
            });
        },
        deletion: function (id) {
            status.update("Sending Delete for #" + id);
            $.ajax({
                url: "/api/habit/" + id,
                method: "DELETE",
                success: function () {
                    habitInputs.clear();
                    status.update("#" + id + " Deleted.");
                    setTimeout(serverRequests.refreshList, 1000);
                }
            });
        },
        refreshList: function () {
            status.update("Refreshing Habit List...");
            $.ajax({
                url: "/api/habit",
                method: "GET",
                success: function (data) {
                    elements.habitList.innerHTML = render.habitList(data);
                    status.update("Fetch Complete.");
                }
            });
        },
        loadSingle: function (id) {
            status.update("Loading #" + id + ".");
            $.ajax({
                url: "/api/habit/" + id,
                method: "GET",
                success: function (data) {
                    habitInputs.setFromModel(data);
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

    habitInputs = {
        setFromModel: function (model) {
            elements.editHabitId.value = model.HabitId;
            elements.editUserId.value = model.UserId;
            elements.editImportance.value = model.Importance;
            elements.editTitle.value = model.Title;
            elements.editDetails.value = model.Details;
        },
        getModel: function () {
            return {
                HabitId: elements.editHabitId.value,
                UserId: elements.editUserId.value,
                Importance: elements.editImportance.value,
                Title: elements.editTitle.value,
                Details: elements.editDetails.value
            };
        },
        clear: function () {
            elements.editHabitId.value = "";
            elements.editUserId.value = "";
            elements.editImportance.value = "";
            elements.editTitle.value = "";
            elements.editDetails.value = "";
        }
    },

    render = {
        habitList: function (listData) {
            var markup = "",
                entry;

            for (var i = 0, len = listData.Habits.length; i < len; i++) {
                entry = listData.Habits[i];

                markup += "<li data-habit-id=\""
                    + entry.HabitId
                    + "\">" + entry.Title + "</li>";

            }

            return markup;
        }
    },

    startupFunction = function () {

        serverRequests.init();
        methods.attachElements(elements);

        serverRequests.refreshList();

        $(elements.habitList).on("click", "li", callbacks.doLoad);
        $(elements.saveButton).on("click", callbacks.doSave);
        $(elements.deleteButton).on("click", callbacks.doDelete);

    };

    return startupFunction;
}(this));