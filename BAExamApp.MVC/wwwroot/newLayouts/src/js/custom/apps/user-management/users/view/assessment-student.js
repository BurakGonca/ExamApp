"use strict";

// Class definition
var KTAssessmentStudent = function () {
    // Shared variables
    const element = document.getElementById('kt_modal_assessment_student');
    const form = document.getElementById('kt_modal_assessment_student_form');
    const modal = new bootstrap.Modal(element);

    // Init add schedule modal
    var initAddExam = () => {

        // Init form validation rules. For more info check the FormValidation plugin's official documentation:https://formvalidation.io/

        var validator = FormValidation.formValidation(
            form,
            {
                fields: {
                    'Content': {
                        validators: {
                            notEmpty: {
                              message: 'Öğrenci değerlendirme alanı zorunludur'
                            }
                        }
                    },
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
        const submitButton = element.querySelector('[data-kt-assessment-modal-action="submit"]');
        submitButton.addEventListener('click', e => {
            e.preventDefault();

            // Validate form before submit
            if (validator) {
                validator.validate().then(function (status) {

                    if (status == 'Valid') {
                        // Show loading indication
                        submitButton.setAttribute('data-kt-indicator', 'on');

                        // Disable button to avoid multiple click 
                        submitButton.disabled = true;
                        form.submit(); // Submit form

                        // Simulate form submission. For more info check the plugin's official documentation: https://sweetalert2.github.io/
                        setTimeout(function () {
                            // Remove loading indication
                            submitButton.removeAttribute('data-kt-indicator');

                            // Enable button
                            submitButton.disabled = false;

                            // Show popup confirmation 
                            Swal.fire({
                                text: "Form başarıyla gönderildi",
                                icon: "success",
                                buttonsStyling: false,
                                confirmButtonText: "Tamam, anlaşıldı",
                                customClass: {
                                    confirmButton: "btn btn-primary"
                                }
                            }).then((result) => {
                                if (result.value) {
                                    validator.resetForm();
                                    form.reset();
                                    modal.hide();
                                }
                            });

                        }, 2000);
                    }
                });
            }
        });

        // Cancel button handler
        const cancelButton = element.querySelector('[data-kt-assessment-modal-action="cancel"]');
        cancelButton.addEventListener('click', e => {
            e.preventDefault();

            Swal.fire({
                title: 'Değişiklikleri kaydetmediniz!',
                text: "Bu sayfadan çıkmak istediğinize emin misiniz?",
                icon: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#d33',
                cancelButtonColor: '#3085d6',
                confirmButtonText: 'Evet, çık!',
                cancelButtonText: 'Hayır, kalmak istiyorum'
            }).then((result) => {
                if (result.value) {
                    validator.resetForm();
                    // Event handler'ı geçici olarak kaldır
                    $('#kt_modal_assessment_student').off('hide.bs.modal');
                    //Seçili fotoğraf varsa kaldır.
                    form.reset();
                    // Modalı kapat
                    modal.hide();
                    // Modal tamamen kapandıktan sonra event handler'ı tekrar ekle
                    $('#kt_modal_assessment_student').on('hidden.bs.modal', function () {
                        addModalCloseConfirmation();
                        $(this).find('form').trigger('reset');
                    });
                }
            });
        });

        // Close button handler
        const closeButton = element.querySelector('[data-kt-assessment-modal-action="close"]');
        closeButton.addEventListener('click', e => {
            e.preventDefault();

            Swal.fire({
                title: 'Değişiklikleri kaydetmediniz!',
                text: "Bu sayfadan çıkmak istediğinize emin misiniz?",
                icon: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#d33',
                cancelButtonColor: '#3085d6',
                confirmButtonText: 'Evet, çık!',
                cancelButtonText: 'Hayır, kalmak istiyorum'
            }).then((result) => {
                if (result.value) {
                    validator.resetForm();
                    // Event handler'ı geçici olarak kaldır
                    $('#kt_modal_assessment_student').off('hide.bs.modal');
                    //Seçili fotoğraf varsa kaldır.
                    form.reset();
                    // Modalı kapat
                    modal.hide();
                    // Modal tamamen kapandıktan sonra event handler'ı tekrar ekle
                    $('#kt_modal_assessment_student').on('hidden.bs.modal', function () {
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
            initAddExam();
            $('.btn-primary[data-bs-toggle="modal"]').on('click', function () {
                var studentId = $(this).data('studentid');
                var studentName = $(this).data('studentname');
                $('input[name="StudentId"]').val(studentId);
                $('#studentName').text('Öğrenciyi Değerlendir - ' + studentName);
            });
        }
    };
}();

function addModalCloseConfirmation() {
    $(".modal-backdrop").remove();
    $('#kt_modal_assessment_student').on('hide.bs.modal', function (e) {
        e.preventDefault();
    });
}

// İlk yüklemede event handler'ı ekle
addModalCloseConfirmation();

$('#kt_modal_assessment_student').on('hidden.bs.modal', function () {
    $(this).find('form').trigger('reset');
});

// On document ready

KTUtil.onDOMContentLoaded(function () {
    KTAssessmentStudent.init();
});