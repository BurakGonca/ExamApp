"use strict";

let admin = null;

// Class definition
var KTUsersUpdateDetails = function () {
    // Shared variables
    const element = document.getElementById('kt_modal_update_user');
    const form = document.getElementById('kt_modal_update_user_form');
    const modal = new bootstrap.Modal(element);

    // Init add schedule modal
    var initUpdateDetails = () => {

        var validator = FormValidation.formValidation(
            form,
            {
                fields: {
                    'NewImage': {
                        validators: {
                            file: {
                                maxSize: 2097152, // 2 MB
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

        // Close button handler
        const closeButton = element.querySelector('[data-kt-users-modal-action="close"]');
        closeButton.addEventListener('click', e => {
            e.preventDefault();

            Swal.fire({
                title: localizedTexts.unsavedChangesTitle,
                text: localizedTexts.unsavedChangesText,
                icon: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#d33',
                cancelButtonColor: '#3085d6',
                confirmButtonText: localizedTexts.confirmButtonText,
                cancelButtonText: localizedTexts.cancelButtonText
            }).then((result) => {
                if (result.value) {
                    validator.resetForm();
                    // Event handler'ı geçici olarak kaldır
                    $('#kt_modal_update_user').off('hide.bs.modal');
                    // Modalı kapat
                    //$('#kt_modal_update_user').modal('hide');
                    modal.hide();
                    // Modal tamamen kapandıktan sonra event handler'ı tekrar ekle
                    $('#kt_modal_update_user').on('hidden.bs.modal', function () {
                        updateModalCloseConfirmation();
                        $(this).find('form').trigger('reset');
                    });
                }
            });
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
                confirmButtonColor: '#d33',
                cancelButtonColor: '#3085d6',
                confirmButtonText: localizedTexts.confirmButtonText,
                cancelButtonText: localizedTexts.cancelButtonText
            }).then((result) => {
                if (result.value) {
                    validator.resetForm();
                    // Event handler'ı geçici olarak kaldır
                    $('#kt_modal_update_user').off('hide.bs.modal');
                    // Modalı kapat
                    /*$('#kt_modal_update_user').modal('hide');*/
                    modal.hide();
                    // Modal tamamen kapandıktan sonra event handler'ı tekrar ekle
                    $('#kt_modal_update_user').on('hidden.bs.modal', function () {
                        updateModalCloseConfirmation();
                        $(this).find('form').trigger('reset');
                    });
                }
            });
        });

        // Submit button handler
        const submitButton = element.querySelector('[data-kt-users-modal-action="submit"]');
        submitButton.addEventListener('click', function (e) {
            // Prevent default button action
            e.preventDefault();

            // Show loading indication
            submitButton.setAttribute('data-kt-indicator', 'on');

            // Disable button to avoid multiple click 
            submitButton.disabled = true;

            // Simulate form submission. For more info check the plugin's official documentation: https://sweetalert2.github.io/
            setTimeout(function () {
                // Remove loading indication
                submitButton.removeAttribute('data-kt-indicator');

                // Enable button
                submitButton.disabled = false;

                // Show popup confirmation 
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

                form.submit(); // Submit form
            }, 2000);
        });
    }

    return {
        // Public functions
        init: function () {
            initUpdateDetails();
        }
    };
}();

async function getCandidate(id) {
    try {
        const response = await $.ajax({
            url: `/candidateAdmin/candidate/update/${id}`,
        });
        return response;
    } catch (error) {
        console.error('Error fetching candidate data:', error);
        throw error;
    }
}

async function loadCandidateData(id) {
    const form = document.getElementById('kt_modal_update_user_form');
    try {
        const candidate = await getCandidate(id);
        const profilePhoto = document.getElementById("profilePhoto");

        form.elements["Id"].value = candidate.id || "";
        form.elements["Image"].value = candidate.image || "";
        form.elements["FirstName"].value = candidate.firstName || "";
        form.elements["LastName"].value = candidate.lastName || "";
        form.elements["Email"].value = candidate.email || "";
        if (candidate.image != null) {
            profilePhoto.style.backgroundImage = 'url("data:image/jpeg;base64,' + candidate.image + '")'
        }
        else {
            profilePhoto.style.backgroundImage = 'url("/images/blank-profile-picture.png")';
        }

    } catch (error) {
        console.error('Error loading candidate data into form:', error);
    }
}


function updateModalCloseConfirmation() {
    $('#kt_modal_update_user').on('hide.bs.modal', function (e) {
        e.preventDefault();
    });
}

// İlk yüklemede event handler'ı ekle
updateModalCloseConfirmation();


$('#kt_modal_update_user').on('hidden.bs.modal', function () {
    $(this).find('form').trigger('reset');
});

// On document ready
KTUtil.onDOMContentLoaded(function () {
    KTUsersUpdateDetails.init();
});