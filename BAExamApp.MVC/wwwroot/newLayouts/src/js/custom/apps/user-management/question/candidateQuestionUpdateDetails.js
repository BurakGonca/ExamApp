// Class definition
var KTUsersCandidateQuestion = function () {
    // Shared variables
    const updateElement = document.getElementById('kt_modal_update_question');
    const updateForm = document.getElementById('kt_modal_update_question_form');
    const updateModal = new bootstrap.Modal(updateElement);

    const detailsElement = document.getElementById('kt_modal_details_question');
    const detailsForm = document.getElementById('kt_modal_details_question_form');
    const detailsModal = new bootstrap.Modal(detailsElement);

    // Init update question modal
    var initUpdateQuestion = () => {
        const cancelButton = updateElement.querySelector('[data-kt-question-modal-action="cancel"]');
        const closeButton = updateElement.querySelector('[data-kt-question-modal-action="close"]');

     
        // Cancel button handler
        cancelButton.addEventListener('click', e => {
            e.preventDefault();

            Swal.fire({
                title: localizedTexts.unsavedChangesTitle,
                text: localizedTexts.unsavedChangesText,
                icon: 'warning',
                showCancelButton: true,
                cancelButtonColor: '#3085d6',
                confirmButtonColor: '#d33',
                confirmButtonText: localizedTexts.confirmButtonText,
                cancelButtonText: localizedTexts.cancelButtonText,
                customClass: {
                    confirmButton: "btn btn-primary",
                    cancelButton: "btn btn-active-light"
                }
            }).then(function (result) {
                if (result.value) {
                    updateForm.reset(); // Reset form
                    updateModal.hide();
                    $(updateForm).find('.invalid-feedback').text("");
                } else if (result.dismiss === 'cancel') {
                    Swal.fire({
                        text: localizedTexts.formNotCancelled,
                        icon: "error",
                        buttonsStyling: false,
                        confirmButtonText: localizedTexts.okButtonText,
                        customClass: {
                            confirmButton: "btn btn-primary",
                        }
                    });
                }
            });
        });

        // Close button handler
        closeButton.addEventListener('click', e => {
            e.preventDefault();

            Swal.fire({
                title: localizedTexts.unsavedChangesTitle,
                text: localizedTexts.unsavedChangesText,
                icon: 'warning',
                showCancelButton: true,
                cancelButtonColor: '#3085d6',
                confirmButtonColor: '#d33',
                confirmButtonText: localizedTexts.confirmButtonText,
                cancelButtonText: localizedTexts.cancelButtonText,
                customClass: {
                    confirmButton: "btn btn-primary",
                    cancelButton: "btn btn-active-light"
                }
            }).then(function (result) {
                if (result.value) {
                    updateForm.reset(); // Reset form
                    updateModal.hide();
                    $(updateForm).find('.invalid-feedback').text("");
                } else if (result.dismiss === 'cancel') {
                    Swal.fire({
                        text: "Formunuz iptal edilmedi!",
                        icon: "error",
                        buttonsStyling: false,
                        confirmButtonText: "Anladım, tamam!",
                        customClass: {
                            confirmButton: "btn btn-primary",
                        }
                    });
                }
            });
        });
    }

    // Init details question modal
    var initDetailQuestion = () => {
        const cancelButton = detailsElement.querySelector('[data-kt-question-details-modal-action="cancel"]');
        const closeButton = detailsElement.querySelector('[data-kt-question-details-modal-action="close"]');

        // Cancel button handler
        cancelButton.addEventListener('click', e => {
            e.preventDefault();
            detailsForm.reset(); // Reset form
            detailsModal.hide();
            $(detailsForm).find('.invalid-feedback').text("");
        });

        // Close button handler
        closeButton.addEventListener('click', e => {
            e.preventDefault();
            detailsForm.reset(); // Reset form
            detailsModal.hide();
            $(detailsForm).find('.invalid-feedback').text("");
        });
    }

    return {
        // Public functions
        init: function () {
            initUpdateQuestion();
            initDetailQuestion();
        }
    };
}();

// On document ready
KTUtil.onDOMContentLoaded(function () {
    KTUsersCandidateQuestion.init();
});
