$(document).ready((function (exports) {

    'use strict';

    var elements = {
        myElement: '#habitsList',
    },

    viewModel = null,
    myElementData = null,
    allHabitsObject = null,

    methods = {
        attachElements: function (elementsObject) {
            for (var key in elementsObject) {
                elementsObject[key] = document.querySelector(elementsObject[key]);
            }
        },
        parseHabitsData: function () {
            myElementData = elements.myElement.dataset.viewModel;
            viewModel = $.parseJSON(myElementData);
            allHabitsObject = viewModel.HabitsWithOccurrences;

            for (var i=0; i < allHabitsObject.length; i++){
                methods.addHabitToHtml(allHabitsObject[i])
            }
        },
        addHabitToHtml: function (toHTML) {
            $('#habitsList').append('<li>' + toHTML.Habit.Title + '</li>');
        },
    },

    startupFunction = function () {
        methods.attachElements(elements);
        methods.parseHabitsData();
    };

    return startupFunction;
}(this)));