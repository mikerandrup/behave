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
        parseHabitsData: function () {
            myElementData = elements.myElement.dataset.viewModel;
            viewModel = $.parseJSON(myElementData);
            console.log(viewModel);
        },
    },

    startupFunction = function () {
        methods.attachElements(elements);
        methods.parseHabitsData();
    };

    return startupFunction;
}(this)));