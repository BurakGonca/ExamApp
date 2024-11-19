"use strict";
// Class definition
    const arrangeTableBody = document.getElementById('arrange-table-tbody')
var KTUsersQuestion = function () {
    // Shared variables
  
    const element = document.getElementById('kt_modal_update_question');
    const form = document.getElementById('kt_modal_update_question_form');
    const modal = new bootstrap.Modal(element);
    const state = document.getElementById('stateInput').value;
    // Init add schedule modal
    var initUpdateQuestion = () => {
        const submitButton = element.querySelector('[data-kt-question-modal-action="submit"]');
      const cancelButton = element.querySelector('[data-kt-question-modal-action="cancel"]');
        const closeButton = element.querySelector('[data-kt-question-modal-action="close"]');
        const arrangeReason = document.getElementById('arrangeReason');
        const arrangeTableContainer = document.getElementById('arrangeTableContainer');
        const arrangeTableCloseButton = document.getElementById('arrangeTableClose');
        if (state === 'Approved') {
            var arrangeReasonInnerHtml = arrangeReason.innerHTML;
            arrangeReason.innerHTML = arrangeReasonInnerHtml + '<div class="fv-row form-floating mb-7">' +
                '<textarea asp-for="ArrangeComment" rows="12" class="form-control form-control-sm form-control-solid" name="ArrangeComment"></textarea>' +
                '<span asp-validation-for="ArrangeComment" class="text-danger"></span>' +
                '<label asp-for="ArrangeComment" class="col-sm-12 col-form-label col-form-label-lg">Düzenleme Sebebi</label>' +
                '<button type="button" class="btn" style="position: absolute; top: 0; right: 0;" id="arrangeHistory">' +
                '<svg width="25px" height="25px" viewBox="0 0 512 512" version="1.1" xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" fill="#000000"><g id="SVGRepo_bgCarrier" stroke-width="0"></g><g id="SVGRepo_tracerCarrier" stroke-linecap="round" stroke-linejoin="round"></g><g id="SVGRepo_iconCarrier"><title>history-list</title><g id="Page-1" stroke="none" stroke-width="1" fill="none" fill-rule="evenodd"><g id="icon" fill="#b2b3b3" transform="translate(33.830111, 42.666667)"><path d="M456.836556,405.333333 L456.836556,448 L350.169889,448 L350.169889,405.333333 L456.836556,405.333333 Z M328.836556,405.333333 L328.836556,448 L286.169889,448 L286.169889,405.333333 L328.836556,405.333333 Z M456.836556,341.333333 L456.836556,384 L350.169889,384 L350.169889,341.333333 L456.836556,341.333333 Z M328.836556,341.333333 L328.836556,384 L286.169889,384 L286.169889,341.333333 L328.836556,341.333333 Z M131.660221,261.176335 C154.823665,284.339779 186.823665,298.666667 222.169889,298.666667 C237.130689,298.666667 251.492003,296.099965 264.837506,291.382887 L264.838264,335.956148 C251.200633,339.466383 236.903286,341.333333 222.169889,341.333333 C175.041086,341.333333 132.37401,322.230407 101.489339,291.345231 L131.660221,261.176335 Z M456.836556,277.333333 L456.836556,320 L350.169889,320 L350.169889,277.333333 L456.836556,277.333333 Z M328.836556,277.333333 L328.836556,320 L286.169889,320 L286.169889,277.333333 L328.836556,277.333333 Z M222.169889,7.10542736e-15 C316.426487,7.10542736e-15 392.836556,76.4100694 392.836556,170.666667 C392.836556,201.752854 384.525389,230.897864 370.003903,256.000851 L317.574603,256.00279 C337.844458,233.356846 350.169889,203.451136 350.169889,170.666667 C350.169889,99.9742187 292.862337,42.6666667 222.169889,42.6666667 C151.477441,42.6666667 94.1698893,99.9742187 94.1698893,170.666667 C94.1698893,174.692297 94.3557269,178.674522 94.7192911,182.605232 L115.503223,161.830111 L145.673112,192 L72.836556,264.836556 L4.97379915e-14,192 L30.1698893,161.830111 L51.989071,183.641815 C51.6671112,179.358922 51.5032227,175.031933 51.5032227,170.666667 C51.5032227,76.4100694 127.913292,7.10542736e-15 222.169889,7.10542736e-15 Z M243.503223,64 L243.503223,159.146667 L297.903223,195.626667 L274.436556,231.04 L200.836556,182.186667 L200.836556,64 L243.503223,64 Z"></path></g></g></g></svg>' +
                '</button></div>';
            arrangeTableCloseButton.addEventListener('click', e => {
                e.preventDefault();
                arrangeTableContainer.style.display = 'none';
            });
            const openArrangeHistoryButton = document.getElementById('arrangeHistory');
            openArrangeHistoryButton.addEventListener('click', e => {
                e.preventDefault();
                arrangeTableContainer.style.display = 'flex';
            });
        }

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
                    'ArrangeComment': {
                        validators: {
                            notEmpty: {
                                message: 'Düzenleme yorum alanı zorunludur'
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
                             document.getElementById("questionAnswerChoices").value = JSON.stringify(questionAnswerChoices);
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
            initUpdateQuestion();
        }
    };
}();

function getQuestionArrangesByQuestionId(questionId) {
    const openArrangeHistoryButton = document.getElementById('arrangeHistory');
    return $.ajax({
        url: '/Admin/Question/QuestionArrangesList',
        data: { questionId: questionId },
        success: function (response) {
            if (openArrangeHistoryButton) {
                if (response.length <= 0) {
                    openArrangeHistoryButton.style.display = 'none';
                }
                else {
                    openArrangeHistoryButton.style.display = 'block';
                    arrangeTableBody.innerHTML = "";
                    response.forEach((arrange, index) => {
                        arrangeTableBody.innerHTML += `<tr>
                    <td>
                        <div>${arrange.comment}</div>
                    </td>
                    <td>${arrange.adminName}</td>
                    <td>${new Date(arrange.createdDate).toLocaleDateString()}</td>
                </tr>`;
                    });
                }
            }         
        },
    });
}



// On document ready
KTUtil.onDOMContentLoaded(function () {
    KTUsersQuestion.init();
});
