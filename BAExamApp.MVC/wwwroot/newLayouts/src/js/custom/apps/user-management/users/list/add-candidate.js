"use strict";

// Class definition
var KTUsersAddUser = function () {
    // Shared variables
    const element = document.getElementById('kt_modal_add_user');
    const form = document.getElementById('kt_modal_add_user_form');
    const modal = new bootstrap.Modal(element);

    // Init add user modal
    var initAddUser = () => {
        var validator = FormValidation.formValidation(
            form,
            {
                fields: {
                    'NewImage': {
                        validators: {
                            file: {
                                maxSize: 2097152, // 5 MB
                                extension: 'jpg,jpeg,png',
                                type: 'image/jpeg,image/png',
                                message: validationMessages.imageInvalid
                            }
                        }
                    },
                    'FirstName': {
                        validators: {
                            notEmpty: {
                                message: validationMessages.firstNameRequired
                            }
                        }
                    },
                    'LastName': {
                        validators: {
                            notEmpty: {
                                message: validationMessages.lastNameRequired
                            }
                        }
                    },
                    'Email': {
                        validators: {
                            notEmpty: {
                                message: validationMessages.emailRequired
                            },
                            emailAddress: {
                                message: validationMessages.emailInvalid
                            }
                        }
                    }
                },
                plugins: {
                    trigger: new FormValidation.plugins.Trigger(),
                    bootstrap: new FormValidation.plugins.Bootstrap5({
                        rowSelector: '.fv-row',
                        eleInvalidClass: '',
                        eleValidClass: ''
                    })
                }
            }
        );

        // Submit button handler
        const submitButton = element.querySelector('[data-kt-users-modal-action="submit"]');
        submitButton.addEventListener('click', e => {
            e.preventDefault();

            // Validate form before submit
            if (validator) {
                validator.validate().then(function (status) {
                    if (status === 'Valid') {
                        submitButton.setAttribute('data-kt-indicator', 'on');
                        submitButton.disabled = true;

                        form.submit(); // Submit form

                        setTimeout(function () {
                            submitButton.removeAttribute('data-kt-indicator');
                            submitButton.disabled = false;

                            Swal.fire({
                                text: localizedTexts.formSubmittedText,
                                icon: "success",
                                buttonsStyling: false,
                                confirmButtonText: localizedTexts.okButtonText,
                                customClass: {
                                    confirmButton: "btn btn-primary"
                                }
                            }).then(function (result) {
                                if (result.isConfirmed) {
                                    modal.hide();
                                }
                            });
                        }, 2000);
                    }
                });
            }
        });

        // Cancel button handler
        const cancelButton = element.querySelector('[data-kt-users-modal-action="cancel"]');
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
                cancelButtonText: localizedTexts.cancelButtonText
            }).then((result) => {
                if (result.isConfirmed) {
                    validator.resetForm();
                    modal.hide();
                }
            });
        });

        // Close button handler
        const closeButton = element.querySelector('[data-kt-users-modal-action="close"]');
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
                cancelButtonText: localizedTexts.cancelButtonText
            }).then((result) => {
                if (result.isConfirmed) {
                    validator.resetForm();
                    modal.hide();
                }
            });
        });
    }

    return {
        // Public functions
        init: function () {
            initAddUser();
        }
    };
}();

$('#kt_modal_add_user').on('hidden.bs.modal', function () {
    $(this).find('form').trigger('reset');
});

// On document ready
KTUtil.onDOMContentLoaded(function () {
    KTUsersAddUser.init();
});
