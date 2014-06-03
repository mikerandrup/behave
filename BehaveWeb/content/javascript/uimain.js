$(document).ready((function (exports) {

    'use strict';

    var elements = {
        myElement: "#habitsList",
    },

    viewModel = null,
    myElementData = null,

    methods = {
        attachElements: function (elementsObject) {
            for (var key in elementsObject) {
                elementsObject[key] = document.querySelector(elementsObject[key]);
            }
        },
        parseHabitsList: function () {
            myElementData = elements.myElement.dataset.viewModel;
            viewModel = $.parseJSON(myElementData);
            console.log(viewModel);
        },
    },

    startupFunction = function () {
        methods.attachElements(elements);
        methods.parseHabitsList();
    };

    return startupFunction;
}(this)));