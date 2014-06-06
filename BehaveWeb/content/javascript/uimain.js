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
            var cssClass = uiRules.getCssClassForStatusCode(habitWithOccurrences.FirstOccurrenceType);

            return('<li class=' + cssClass + '>' + habitWithOccurrences.Habit.Title + '</li>');
        }
    },

    uiRules = {
        getCssClassForStatusCode: function (statusCode) {
            if(typeof(statusCode) !== "string"){
                statusCode = statusCode.toString();
            }
            var cssClass = "";

            if ( viewModel.UiRulesForStatusCodes[statusCode] != undefined ){
                cssClass = viewModel.UiRulesForStatusCodes[statusCode].cssClass;
            }

            return cssClass;
        }


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