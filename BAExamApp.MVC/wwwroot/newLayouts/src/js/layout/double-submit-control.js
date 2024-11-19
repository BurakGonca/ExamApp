"use strict";

// Class definition
var KTFormDoubleClick = function () {

    let isSubmitting = false;

    const forms = document.querySelectorAll("form");
    forms.forEach(function (form) {
        form.addEventListener("submit", function (e) {

            var initFormClick = () => {

                if (isSubmitting) {
                    e.preventDefault();
                } else {
                    isSubmitting = true;
                }
            }
        });
    });

    return {
        // Public functions
        init: function () {
            initFormClick();
        }
    };
}();

KTUtil.onDOMContentLoaded(function () {
    KTFormDoubleClick.init();
});