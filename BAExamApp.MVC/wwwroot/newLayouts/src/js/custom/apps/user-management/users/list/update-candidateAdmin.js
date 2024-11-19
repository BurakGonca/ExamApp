"use strict";

let candidateadmin = null;

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
                                message: 'Lütfen maksimum 2 MB boyutunda ve uygun dosya biçiminde bir fotoğraf seçiniz (png, jpg, jpeg)'
                            }
                        }
                    },
                    'FirstName': {
                        validators: {
                            notEmpty: {
                                message: 'İsim alanı zorunludur'
                            }
                        }
                    },
                    'LastName': {
                        validators: {
                            notEmpty: {
                                message: 'Soyisim alanı zorunludur'
                            }
                        }
                    },
                    'Email': {
                        validators: {
                            notEmpty: {
                                message: 'Mail Adresi alanı zorunludur'
                            },
                            emailAddress: {
                                message: 'Lütfen geçerli bir mail adresi giriniz'
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
                if (result.isConfirmed) {
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

async function getCandidateAdmin(candidateadminid) {

    return $.ajax({
        url: '/candidateAdmin/candidateAdmin/getCandidateAdmin',
        data: { candidateadminid: candidateadminid },
    });
}

async function loadCandidateAdminData(candidateadminid) {
    const form = document.getElementById('kt_modal_update_user_form');
    candidateadmin = await getCandidateAdmin(candidateadminid);
    let profilePhoto = document.getElementById("profilePhoto");
    form.elements["Id"].value = candidateadmin.id;
    if (candidateadmin.image !== null && candidateadmin.image !== "0" && candidateadmin.image !== "1") {
        profilePhoto.style.backgroundImage = 'url("data:image/jpeg;base64,' + candidateadmin.image + '")';
    } else {
        profilePhoto.style.backgroundImage = 'url("/images/blank-profile-picture.png")';
    }
    form.elements["Image"].value = candidateadmin.image;
    form.elements["FirstName"].value = candidateadmin.firstName;
    form.elements["LastName"].value = candidateadmin.lastName;
    //form.elements["Gender"].value = candidateadmin.gender;
    //form.elements["DateOfBirth"].value = candidateadmin.dateOfBirth.split('T')[0];
    form.elements["Email"].value = candidateadmin.email;

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






















