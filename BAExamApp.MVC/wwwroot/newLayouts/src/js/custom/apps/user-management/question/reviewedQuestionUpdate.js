"use strict";
// Class definition
const KTUsersQuestionReviewed = function () {
    // Shared variables2

    const element = document.getElementById('kt_modal_update_question_reviewed');
    const form = document.getElementById('kt_modal_update_question_reviewed_form');
    const modal = new bootstrap.Modal(element);

    // Init add schedule modal
    var initUpdateQuestionReviewed = () => {
        const submitButton = element.querySelector('[data-kt-question-reviewed-modal-action="submitReviewed"]');
        const cancelButton = element.querySelector('[data-kt-question-reviewed-modal-action="cancelReviewed"]');
        const closeButton = element.querySelector('[data-kt-question-reviewed-modal-action="closeReviewed"]');
        // Init form validation rules. For more info check the FormValidation plugin's official documentation:https://formvalidation.io/
        try {
            var validator = FormValidation.formValidation(
                form,
                {
                    fields: {
                        'ProductId': {
                            validators: {
                                notEmpty: {
                                    message: '<p>Ürün alanı zorunludur</p>'
                                }
                            }
                        },
                        'SubjectId': {
                            validators: {
                                notEmpty: {
                                    message: 'Konu alanı zorunludur'
                                }
                            }
                        },
                        'SubtopicId': {
                            validators: {
                                notEmpty: {
                                    message: 'Alt konu alanı zorunludur'
                                },
                            }
                        },
                        'QuestionType': {
                            validators: {
                                notEmpty: {
                                    message: 'Sorutipini giriniz alanı zorunludur'
                                }
                            }
                        },
                        'QuestionDifficultyId': {
                            validators: {
                                notEmpty: {
                                    message: 'Lütfen soru zorluğu alanını giriniz'
                                }
                            }
                        },
                        'Target': {
                            validators: {
                                notEmpty: {
                                    message: 'Hedef alanı zorunludur'
                                }
                            }
                        },
                        'Gains': {
                            validators: {
                                notEmpty: {
                                    message: 'Kazanım alanı zorunludur'
                                }
                            }
                        },
                        'Content': {
                            validators: {
                                notEmpty: {
                                    message: 'İçerik alanı zorunludur'
                                }
                            }
                        },
                        'ReviseComment': { 
                            validators: {
                                notEmpty: {
                                    message : 'Revize yorum alanı zorunludur'
                                }
                            }

                        },
                        'NewImage': {
                            validators: {
                                file: {
                                    maxSize: 1024 * 1024, // 1 MB
                                    message: 'Dosya boyutu 1 MB\'ı geçemez'
                                },
                                file: {
                                    extension: 'jpg,jpeg,png', // İzin verilen dosya türleri
                                    message: 'Sadece JPG, JPEG ve PNG dosyaları desteklenmektedir'
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
        } catch (e) {
            console.log("hata")
        }

        // Submit button handler
        submitButton.addEventListener('click', e => {
            e.preventDefault();
           
            // Validate form before submit
            if (validator) {
                validator.validate().then(function (status) {
                    console.log('validated!');

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
                                text: "Form başarıyla gönderildi!",
                                icon: "success",
                                buttonsStyling: false,
                                confirmButtonText: "Anladım, tamam!",
                                customClass: {
                                    confirmButton: "btn btn-primary"
                                }
                            }).then(function (result) {
                                if (result.isConfirmed) {
                                    modal.hide();
                                }
                                    console.log("Then içerisi kontrol");
                            });
                            document.getElementById("questionAnswerChoicesReviewed").value = JSON.stringify(questionAnswerChoicesReviewed);
                            // Submit form
                            form.submit();
                        }, 2000);
                    } else {
                        // Show popup warning. For more info check the plugin's official documentation: https://sweetalert2.github.io/
                        Swal.fire({
                            text: "Üzgünüm, bazı hatalar tespit edildi. Lütfen tekrar deneyin.",
                            icon: "error",
                            buttonsStyling: false,
                            confirmButtonText: "Anladım, tamam!",
                            customClass: {
                                confirmButton: "btn btn-primary"
                            }
                        });
                    }
                });
            }
        });
        // Cancel button handler
        cancelButton.addEventListener('click', e => {
            e.preventDefault();

            Swal.fire({
                text: "İptal etmek istediğinizden emin misiniz?",
                icon: "warning",
                showCancelButton: true,
                buttonsStyling: false,
                confirmButtonText: "Evet, iptal et!",
                cancelButtonText: "Hayır, geri dön",
                customClass: {
                    confirmButton: "btn btn-primary",
                    cancelButton: "btn btn-active-light"
                }
            }).then(function (result) {
                if (result.value) {
                    form.reset(); // Reset form			
                    modal.hide();

                    $(form).find('.invalid-feedback').text("");

                    // Tüm form alanlarının kenarlıklarını temizle

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

        // Close button handler

        closeButton.addEventListener('click', e => {
            e.preventDefault();

            Swal.fire({
                text: "İptal etmek istediğinizden emin misiniz?",
                icon: "warning",
                showCancelButton: true,
                buttonsStyling: false,
                confirmButtonText: "Evet, iptal et!",
                cancelButtonText: "Hayır, geri dön",
                customClass: {
                    confirmButton: "btn btn-primary",
                    cancelButton: "btn btn-active-light"
                }
            }).then(function (result) {
                if (result.value) {
                    form.reset(); // Reset form			
                    modal.hide();
                    $(form).find('.invalid-feedback').text("");
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
    return {
        // Public functions
        init: function () {
            initUpdateQuestionReviewed();
        }
    };
}();

// On document ready
KTUtil.onDOMContentLoaded(function () {
    KTUsersQuestionReviewed.init();
});
