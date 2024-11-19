"use strict";

// Class definition
var KTUsersUpdateDetails = function () {
    // Shared variables
    const element = document.getElementById('updateProductModal');
    //const form = element.querySelector('#kt_modal_update_user_form');
    const form = document.getElementById('kt_modal_update_product_form');
    const modal = new bootstrap.Modal(element);
    var validator;

    // Init add schedule modal
    var initUpdateDetails = () => {
        
         validator = FormValidation.formValidation(
            form,
            {
                fields: {
                    'Name': {
                        validators: {
                            notEmpty: {
                                message: 'İsim alanı zorunludur'
                            }
                        }
                    },
                    'TechnicalUnitId': {
                        validators: {
                            notEmpty: {
                                message: 'Teknik birimler alanı zorunludur'
                            }
                        }
                    },
                    //'SubjectIds': {
                    //    validators: {
                    //        notEmpty: {
                    //            message: 'Konu alanı zorunludur'
                    //        }
                    //    }
                    //},
                    'IsActive': {
                        validators: {
                            notEmpty: {
                                message: 'Eğitim aktiflik alanı zorunludur'
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
        element.addEventListener('hide.bs.modal', function () {
            // Reset form validation state when modal is hidden
            if (validator) {
                validator.resetForm();
            }
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
                cancelButtonText: localizedTexts.cancelButtonText,
                customClass: {
                    confirmButton: "btn btn-primary",
                    cancelButton: "btn btn-active-light"
                }
            }).then(function (result) {
                if (result.value) {
                    form.reset(); // Reset form	
                    modal.hide(); // Hide modal				
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
                    modal.hide(); // Hide modal				
                } 
            });
        });

        // Submit button handler
        const submitButton = element.querySelector('[data-kt-users-modal-action="submit"]');
        submitButton.addEventListener('click', function (e) {
            // Prevent default button action
            e.preventDefault();

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

                            form.submit(); // Submit form
                        }, 2000);
                    } 
                });
            }
        });
    }

    return {
        // Public functions
        init: function () {
            initUpdateDetails();
        }
    };
}();

async function loadProductData(id) {
    const form = document.getElementById('kt_modal_update_product_form');

    const product = await getProduct(id);

    form.elements["Id"].value = product.id;
    form.elements["Name"].value = product.name;
    form.elements["TechnicalUnitId"].value = product.technicalUnitId;
    /*form.elements["SubjectIds"].value = product.subjectIds;*/
    

    const productSubjectSelect = form.elements["SubjectIds"];

    for (let option of productSubjectSelect.options) {
        option.selected = false;
    }

    for (let talentId of product.subjectIds) {
        for (let option of productSubjectSelect.options) {
            if (option.value == talentId) {
                option.selected = true;
                break;
            }
        }
    }

    const event = new Event('change');
    productSubjectSelect.dispatchEvent(event);

    form.elements["IsActive"].value = product.isActive;

    console.log('ID:', product.id);
    console.log('Name:', product.Name);
    console.log('TechnicalUnitId:', product.technicalUnitId);
    console.log('SubjectIds:', product.SubjectIds);
    console.log('IsActive', product.IsActive);

    

    
    
}

async function getProduct(productId) {

    return $.ajax({
        url: '/Admin/Product/GetProduct',
        data: { productId: productId }
    });
}

// On document ready
KTUtil.onDOMContentLoaded(function () {
    KTUsersUpdateDetails.init();
});