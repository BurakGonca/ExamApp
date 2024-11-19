"use strict";

// Class definition
var KTUsersAddUser = function () {
    // Shared variables
    const element = document.getElementById('kt_modal_add_user');
    const form = document.getElementById('kt_modal_add_user_form');
    const modal = new bootstrap.Modal(element);

    // Init add schedule modal
    var initAddUser = () => {

        // Init form validation rules. For more info check the FormValidation plugin's official documentation:https://formvalidation.io/

        var validator = FormValidation.formValidation(
            form,
            {
                fields: {
                    'NewImage': {
                        validators: {
                            file: {
                                extension: 'png,jpg,jpeg',
                                type: 'image/png,image/jpg,image/jpeg',
                                maxSize: 524288, // 512 kb
                                message: 'Lütfen maksimum 512 kb boyutunda ve uygun dosya biçiminde bir fotoğraf seçiniz (png, jpg, jpeg)'
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
                    },
                    'Gender': {
                        validators: {
                            notEmpty: {
                                message: 'Cinsiyet alanı zorunludur'
                            }
                        }
                    }
                    //'DateOfBirth': {
                    //    validators: {
                    //        notEmpty: {
                    //            message: 'Doğum tarihi alanı zorunludur'
                    //        }
                    //    }
                    //},
                    //'CityId': {
                    //    validators: {
                    //        notEmpty: {
                    //            message: 'Şehir alanı zorunludur'
                    //        }
                    //    }
                    //},
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
        submitButton.addEventListener('click', function (e) {
            // Prevent default button action
            e.preventDefault();

            if (validator) {
                validator.validate().then(function (status) {

                    if (status == 'Valid') {

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
                if (result.value) {
                    validator.resetForm();
                    // Event handler'ı geçici olarak kaldır
                    $('#kt_modal_add_user').off('hide.bs.modal');
                    // Modalı kapat
                    //$('#kt_modal_add_user').modal('hide');
                    modal.hide();
                    // Modal tamamen kapandıktan sonra event handler'ı tekrar ekle
                    $('#kt_modal_add_user').on('hidden.bs.modal', function () {
                        addModalCloseConfirmation();
                        $(this).find('form').trigger('reset');
                    });
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
                if (result.value) {
                    validator.resetForm();
                    // Event handler'ı geçici olarak kaldır
                    $('#kt_modal_add_user').off('hide.bs.modal');
                    // Modalı kapat
                    //$('#kt_modal_add_user').modal('hide');
                    modal.hide();
                    // Modal tamamen kapandıktan sonra event handler'ı tekrar ekle
                    $('#kt_modal_add_user').on('hidden.bs.modal', function () {
                        addModalCloseConfirmation();
                        $(this).find('form').trigger('reset');
                    });
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

function addModalCloseConfirmation() {
    $('#kt_modal_add_user').on('hide.bs.modal', function (e) {
        e.preventDefault();
    });
}

// İlk yüklemede event handler'ı ekle
addModalCloseConfirmation();

$('#kt_modal_add_user').on('hidden.bs.modal', function () {
    $(this).find('form').trigger('reset');
});


function addOtherEmail() {

    //Create otherMails html
    let template = `<div class="fv-row mb-7">
<div class="d-flex align-items-center gap-3">
<!--begin::Input group-->
<div class="form-floating w-75">
<input type="email" class="form-control form-control-solid mb-3 mb-lg-0" id="OtherEmails" name="OtherEmails" placeholder="ad.soyad@bilgeadam.com" required />
<label for="OtherEmails" class="required fw-bold fs-6 mb-2 text-muted">Diğer Mail Adresi</label>
<span data-valmsg-for="OtherEmails" class="text-danger"></span>
</div>
<!--end::Input group-->
<div>
<a class="btn btn-danger btn-lg" onclick="removeMail(this)"><span class="h5 text-light">X</span></a>
</div>
</div>
</div>`;

    //Append created otherMails
    $("#otherEmails").append(template);

}
function removeMail(prop) {
    //Removing otherEmail
    $(prop).parent().parent().parent().remove();
}

// Bu fonksiyon, tarih alanındaki değeri kontrol eder
//function validateDateCreate() {
//    var inputDate = new Date(document.getElementById('DateOfBirth').value);
//    var currentDate = new Date();
//    // Doğum tarihi gelecekte veya bugün olamaz
//    if (inputDate >= currentDate) {
//        document.getElementById('DateOfBirth').value = ''; // Tarihi temizle
//        document.getElementById('error-message').textContent = 'İleri tarihli giremezsiniz.'; // Hata mesajı göster
//        return false;
//    }
//    // Eğer doğum tarihi geçerliyse, hata mesajını kaldır
//    document.getElementById('error-message').textContent = '';
//    return true;
//}
//// Tarih alanının değeri değiştiğinde validateDateCreate() fonksiyonunu çağır
//document.getElementById('DateOfBirth').addEventListener('change', validateDateCreate);
//// Form gönderilmeden önce validateDateCreate() fonksiyonunu çağırır
//document.querySelector('form').addEventListener('submit', function (event) {
//    if (!validateDateCreate()) {
//        event.preventDefault();
//    }
//});

// On document ready
KTUtil.onDOMContentLoaded(function () {
    KTUsersAddUser.init();
});