"use strict";

// Class definition
var KTUsersUpdateDetails = function () {
    // Shared variables
    const element = document.getElementById('kt_modal_update_student');
    //const form = element.querySelector('#kt_modal_update_user_form');
    const form = document.getElementById('kt_modal_update_student_form');
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
                        //},
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

        // Close button handler
        const closeButton = element.querySelector('[data-kt-users-modal-action="closes"]');
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
                    // Event handler'ı geçici olarak kaldır
                    $('#kt_modal_update_student').off('hide.bs.modal');
                    // Modalı kapat
                    $('#kt_modal_update_student').modal('hide');
                    // Modal tamamen kapandıktan sonra event handler'ı tekrar ekle
                    $('#kt_modal_update_student').on('hidden.bs.modal', function () {
                        addModalCloseConfirmation();
                        $(this).find('form').trigger('reset');
                    });
                }
            });
        });

        // Cancel button handler
        const cancelButton = element.querySelector('[data-kt-users-modal-action="cancels"]');
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
                    // Event handler'ı geçici olarak kaldır
                    $('#kt_modal_update_student').off('hide.bs.modal');
                    // Modalı kapat
                    $('#kt_modal_update_student').modal('hide');
                    // Modal tamamen kapandıktan sonra event handler'ı tekrar ekle
                    $('#kt_modal_update_student').on('hidden.bs.modal', function () {
                        addModalCloseConfirmation();
                        $(this).find('form').trigger('reset');
                    });
                }
            });
        });

        // Submit button handler
        const submitButton = element.querySelector('[data-kt-users-modal-action="submits"]');
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

function startLoader() {
    var loader = document.getElementById('loader-notification');
    loader.style.display = 'block';
}

function stopLoader() {
    var loader = document.getElementById('loader-notification');
    loader.style.display = 'none';
}

async function loadStudentData(id) {
    startLoader();
    const form = document.getElementById('kt_modal_update_student_form');

    const profilePhoto = document.getElementById("profilePhoto");
    try {
    const student = await getStudent(id);
    console.log(student);
    form.elements["Id"].value = student.id;

    if (student.originalImage !== null && student.originalImage.length > 0) {
        profilePhoto.style.backgroundImage = 'url("data:image/jpeg;base64,' + student.originalImage + '")';
    } else {
        profilePhoto.style.backgroundImage = 'url("/images/blank-profile-picture.png")';
    }
    form.elements["FirstName"].value = student.firstName;
    form.elements["Image"].value = student.originalImage;
    form.elements["LastName"].value = student.lastName;
    form.elements["Email"].value = student.email;
    form.elements["Gender"].value = student.gender;
    //form.elements["DateOfBirth"].value = student.dateOfBirth.split("T")[0];
    //form.elements["CityId"].value = student.cityId;

    const classroomSelect = form.querySelector('select[name="ClassroomIds"]');
    classroomSelect.innerHTML = '';  
    
    student.classroomSelectList.forEach(classroom => {
        const option = document.createElement('option');
        option.value = classroom.value;  
        option.textContent = classroom.text;  

        if (student.classroomIds.includes(option.value)) {
            option.selected = true;
        } else {
            option.selected = false;  
        }
        classroomSelect.appendChild(option);  

    });
    } catch (error) {
        console.error('Hata yüklenirken:', error);
    } finally {
        stopLoader();
    }
}

function addModalCloseConfirmation() {
    $('#kt_modal_update_student').on('hide.bs.modal', function (e) {
        e.preventDefault();
        showModalCloseConfirmation();
    });
}

// İlk yüklemede event handler'ı ekle
addModalCloseConfirmation();

$('#kt_modal_update_student').on('hidden.bs.modal', function () {
    $(this).find('form').trigger('reset');
});
async function getStudent(studentId) {

    return $.ajax({
        url: '/Admin/Student/GetStudent',
        data: { studentId: studentId }
    });
}

// On document ready
KTUtil.onDOMContentLoaded(function () {
    KTUsersUpdateDetails.init();
});