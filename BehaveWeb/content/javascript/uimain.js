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
        },
        getGestureFromAmount: function (amount) {
            if (amount < 0.125) {
                return "farLeft";
            }
            else if (amount >= 0.125 && amount < 0.375) {
                return "slightLeft";
            }
            else if (amount >= 0.375 && amount < 0.625) {
                return "tap"
            }
            else if (amount >= 0.625 && amount < 0.875) {
                return "slightRight";
            }
            else if (amount >= 0.875) {
                return "farRight";
            }
            else {
                return "";
            }
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
        },
        getRulesForGesture: function (gestureName) {
            if (viewModel.UiRulesForGestures[gestureName] != undefined) {
                return viewModel.UiRulesForGestures[gestureName];
            }

            throw new Exception("Invalid gesture name " + gestureName);
        }
    },

    callbacks = {
        jumpToDate: function (evt) {
            var el = evt.currentTarget,
                chosenDateString = el.value;
            
            if(chosenDateString.length > 0){                
                window.location.href = "?date=" + window.encodeURIComponent(chosenDateString);
            }
            else{
                console.log("empty date string");
                return false;
            }
        },
        handleFinalPosition: function (evt) {
            var el = evt.currentTarget,
                xCoord = evt.clientX,
                elWidth = el.clientWidth,
                amount = xCoord/elWidth;

            var gestureName = methods.getGestureFromAmount(amount);

            console.log(amount, gestureName);

            var rules = uiRules.getRulesForGesture(gestureName),
                occurrenceCode = rules.occurrenceCode,
                requiresReason = rules.requiresReason;

            console.log(occurrenceCode, requiresReason);

            var cssClassName = uiRules.getCssClassForStatusCode(occurrenceCode);

            el.className = cssClassName;
        }
    },

    startupFunction = function () {
        methods.attachElements(elements);
        methods.parseHabitsData();

        $(elements.dateChooser).on("blur", callbacks.jumpToDate);
        $(elements.habitListElement).on("click", "li", callbacks.handleFinalPosition);
    };

    return startupFunction;
}(this)));