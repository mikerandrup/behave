$(document).ready((function (exports) {

    'use strict';

    var elements = {
        myElement: "selector.forMyElement"
    },

    viewModel = null,

    methods = {
        attachElements: function (elementsObject) {
            for (var key in elementsObject) {
                elementsObject[key] = document.querySelector(elementsObject[key]);
            }
            console.log(elementsObject);
        }
    },

    startupFunction = function () {
        methods.attachElements(elements);
    };

    return startupFunction;
}(this)));