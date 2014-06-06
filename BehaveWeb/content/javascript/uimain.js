$(document).ready((function (exports) {

    'use strict';

    var elements = {
        habitListElement: '#habitsList',
        dateChooser: "#dateChooser"
    },

    viewModel = null,
    myHabitsData = null,
    allHabitsObject = null,

    methods = {
        attachElements: function (elementsObject) {
            for (var key in elementsObject) {
                elementsObject[key] = document.querySelector(elementsObject[key]);
            }
        },
        parseHabitsData: function () {
            myHabitsData = elements.habitListElement.dataset.viewModel;
            viewModel = $.parseJSON(myHabitsData);
            console.log(viewModel);
            allHabitsObject = viewModel.HabitsWithOccurrences;

            var habitListMarkup = "";

            for (var i=0; i < allHabitsObject.length; i++){
                habitListMarkup += methods.createHtmlForHabit(allHabitsObject[i])
            }
            elements.habitListElement.innerHTML = habitListMarkup;
        },
        createHtmlForHabit: function (habitWithOccurrences) {
            return('<li>' + habitWithOccurrences.Habit.Title + '</li>');
        },
    },

    callbacks = {
        jumpToDate: function (evt) {
            var el = evt.currentTarget,
                chosenDateString = el.value;
            window.location.href = "?date=" + window.encodeURIComponent(chosenDateString);
        }
    },

    startupFunction = function () {
        methods.attachElements(elements);
        methods.parseHabitsData();

        $(elements.dateChooser).on("blur", callbacks.jumpToDate);
    };

    return startupFunction;
}(this)));