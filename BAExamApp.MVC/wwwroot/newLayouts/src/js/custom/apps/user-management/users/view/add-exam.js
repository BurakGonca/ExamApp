"use strict";

// Class definition
var KTExamsAddExam = function () {
    // Shared variables
    const element = document.getElementById('kt_modal_create_exam');
    const form = document.getElementById('kt_modal_add_exam_form');
    const modal = new bootstrap.Modal(element);

    // Init add schedule modal
    var initAddExam = () => {

        // Init form validation rules. For more info check the FormValidation plugin's official documentation:https://formvalidation.io/
        // ExamDateTime için Flatpickr
        $("#ExamDateTime").flatpickr({
            enableTime: true,
            minDate: "today",
            dateFormat: "d/m/Y H:i",
            locale: "tr",
            time_24hr: true,
            allowInput: true, // Klavye ile girişe izin ver
            onChange: function (selectedDates, dateStr, instance) {
                validator.updateFieldStatus('ExamDateTime', 'NotValidated');
            }
        });

        $("#ExamDuration").flatpickr({
            enableTime: true,
            noCalendar: true,
            dateFormat: "H:i",
            time_24hr: true,
            allowInput: true, // Klavye ile girişe izin ver
            onOpen: function (selectedDates, dateStr, instance) {
                const examDurationValue = $('#ExamDuration').val();
                if (examDurationValue) {
                    instance.setDate(examDurationValue); // Flatpickr'a ayarla
                }
            }
        });


        var validator = FormValidation.formValidation(
            form,
            {
                fields: {
                    'ExamDateTime': {
                        validators: {
                            notEmpty: {
                                message: 'Sınav tarihi ve saati alanı zorunludur'
                            }
                            ,
                            callback: {
                                message: 'Geçmiş tarih ve saat seçilemez',
                                callback: function (input) {
                                    var selectedDate = flatpickr.parseDate(input.value, "d/m/Y H:i");
                                    var currentDate = new Date();
                                    return selectedDate >= currentDate;
                                }
                            }
                        }
                    },
                    'ExamDuration': {
                        validators: {
                            notEmpty: {
                                message: 'Sınav süresi alanı zorunludur'
                            }
                        }
                    },
                    'MaxScore': {
                        validators: {
                            notEmpty: {
                                message: 'Maksimum puan alanı zorunludur'
                            }
                        }
                    },
                    'BonusScore': {
                        validators: {
                            notEmpty: {
                                message: 'Bonus puan alanı zorunludur'
                            }
                        }
                    },
                    'ExamType': {
                        validators: {
                            notEmpty: {
                                message: 'Sınav tipi alanı zorunludur'
                            }
                        }
                    },
                    'ExamRuleId': {
                        validators: {
                            notEmpty: {
                                message: 'Sınav kuralı alanı zorunludur'
                            }
                        }
                    },
                    'ExamCreationType': {
                        validators: {
                            notEmpty: {
                                message: 'Sınav oluşturma türü alanı zorunludur'
                            }
                        }
                    },
                    'ExamClassroomsIds': {
                        validators: {
                            notEmpty: {
                                message: 'Lütfen en az bir sınıf seçiniz'
                            }
                        }
                    },
                    'Description': {
                        validators: {
                            notEmpty: {
                                message: 'Lütfen sınav açıklamasını giriniz'
                            }
                        }
                    },
                    'StudentIds': {
                        validators: {
                            notEmpty: {
                                message: 'Lütfen en az bir öğrenci giriniz'
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

        // Switch element change event
        const switchElement = $("#ogrenci");
        const studentArea = $("#studentArea");
        const ogrenciLabel = $("#ogrenciLabel");
        const sinifLabel = $("#sinifLabel");

        switchElement.on('change', function () {
            if ($(this).is(':checked')) {
                studentArea.hide();
                sinifLabel.css('font-weight', 'bold');
                ogrenciLabel.css('font-weight', 'normal');
                // Sınıf seçildiğinde form-validation kurallarını devre dışı bırak
                validator.disableValidator('Description');
                validator.disableValidator('StudentIds');
            } else {
                studentArea.show();
                ogrenciLabel.css('font-weight', 'bold');
                sinifLabel.css('font-weight', 'normal');
                // Öğrenci seçildiğinde form-validation kurallarını etkinleştir
                validator.enableValidator('Description');
                validator.enableValidator('StudentIds');
            }
        });

        // Initialize switchElement state
        if (switchElement.is(':checked')) {
            studentArea.hide();
            sinifLabel.css('font-weight', 'bold');
            ogrenciLabel.css('font-weight', 'normal');
            validator.disableValidator('Description');
            validator.disableValidator('StudentIds');
        } else {
            studentArea.show();
            ogrenciLabel.css('font-weight', 'bold');
            sinifLabel.css('font-weight', 'normal');
            validator.enableValidator('Description');
            validator.enableValidator('StudentIds');
        }


        // Submit button handler
        const submitButton = element.querySelector('[data-kt-exam-modal-action="submit"]');
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
                                text: localizedTexts.formSubmittedText,
                                icon: "success",
                                buttonsStyling: false,
                                confirmButtonText: localizedTexts.okButtonText,
                                customClass: {
                                    confirmButton: "btn btn-primary"
                                }
                            }).then((result) => {
                                if (result.value) {
                                    $('#kt_modal_create_exam').off('hide.bs.modal');
                                    modal.hide();
                                    validator.resetForm();
                                    form.reset();
                                }
                            });

                        }, 2000);
                    }
                });
            }
        });

        // Cancel button handler
        const cancelButton = element.querySelector('[data-kt-exam-modal-action="cancel"]');
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
                cancelButtonText: localizedTexts.cancelButtonText,
            }).then((result) => {
                if (result.value) {
                    validator.resetForm();
                    // Event handler'ı geçici olarak kaldır
                    $('#kt_modal_create_exam').off('hide.bs.modal');
                    //Seçili fotoğraf varsa kaldır.
                    form.reset();
                    // Modalı kapat
                    modal.hide();
                    // Modal tamamen kapandıktan sonra event handler'ı tekrar ekle
                    $('#kt_modal_create_exam').on('hidden.bs.modal', function () {
                        addModalCloseConfirmation();
                        $(this).find('form').trigger('reset');
                    });
                }
            });
        });

        // Close button handler
        const closeButton = element.querySelector('[data-kt-exam-modal-action="close"]');
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
                cancelButtonText: localizedTexts.cancelButtonText,
            }).then((result) => {
                if (result.value) {
                    validator.resetForm();
                    // Event handler'ı geçici olarak kaldır
                    $('#kt_modal_create_exam').off('hide.bs.modal');
                    //Seçili fotoğraf varsa kaldır.
                    form.reset();
                    // Modalı kapat
                    modal.hide();
                    // Modal tamamen kapandıktan sonra event handler'ı tekrar ekle
                    $('#kt_modal_create_exam').on('hidden.bs.modal', function () {
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
        }
    };
}();

function addModalCloseConfirmation() {
    $('#kt_modal_create_exam').on('hide.bs.modal', function (e) {
        e.preventDefault();
    });
}

// İlk yüklemede event handler'ı ekle
addModalCloseConfirmation();

$('#kt_modal_create_exam').on('hidden.bs.modal', function () {
    $(this).find('form').trigger('reset');
});


let examRules = [];

async function getExamRules(selectedExamTypeId) {
    return $.ajax({
        url: '/Admin/Exam/GetExamRulesByExamType',
        data: { examTypeId: selectedExamTypeId }
    });
}

async function onExamTypeChange() {
    examRules = examRules ? await getExamRules($("#ExamType").val()) : examRules;
    populateSelectList("ExamRuleId", examRules);

    examRules = [];
};

async function populateSelectList(selectListId, data) {
    let selectListOptions = data.map((item, index) => `<option value="${item.value}">${item.text}</option>`);
    let selectList = `<option value="" disabled="" selected="">--- Seçiniz ---</option>`.concat(selectListOptions);
    document.getElementById(selectListId).innerHTML = selectList;
}

async function GetStudentsByClassroom() {
    var classroomStudents = {};
    var selectedStudents = [];

    $('#ExamClassroomsIds').on('change', function () {
        var selectedClassrooms = $(this).val();

        selectedStudents = $('#studentSelect').val() || [];

        var allStudents = [];
        selectedClassrooms.forEach(function (classroomId) {
            if (classroomStudents[classroomId]) {
                allStudents = allStudents.concat(classroomStudents[classroomId]);
            } else {
                $.ajax({
                    url: '/Trainer/Exam/GetStudentsByClassroom',
                    type: 'GET',
                    data: { classroomId: classroomId },
                    success: function (students) {
                        classroomStudents[classroomId] = students;
                        allStudents = allStudents.concat(students);
                        updateStudentSelect(allStudents);
                    }
                });
            }
        });

        updateStudentSelect(allStudents);
    });

    function updateStudentSelect(students) {
        var studentSelect = $('#studentSelect');
        studentSelect.empty();

        students.forEach(function (student) {
            var isSelected = selectedStudents.indexOf(student.value) > -1;
            var newOption = new Option(student.text, student.value, isSelected, isSelected);
            studentSelect.append(newOption);
        });

        studentSelect.trigger('change');
    }

    const switchElement = $("#ogrenci");
    const studentArea = $("#studentArea");
    const ogrenciLabel = $("#ogrenciLabel");
    const sinifLabel = $("#sinifLabel");

    switchElement.on('change', function () {
        if ($(this).is(':checked')) {
            studentArea.hide();
            sinifLabel.css('font-weight', 'bold');
            ogrenciLabel.css('font-weight', 'normal');
        } else {
            studentArea.show();
            ogrenciLabel.css('font-weight', 'bold');
            sinifLabel.css('font-weight', 'normal');
        }
    });

    if (switchElement.is(':checked')) {
        studentArea.hide();
        sinifLabel.css('font-weight', 'bold');
        ogrenciLabel.css('font-weight', 'normal');
    } else {
        studentArea.show();
        ogrenciLabel.css('font-weight', 'bold');
        sinifLabel.css('font-weight', 'normal');
    }
};


//// Bu fonksiyon, tarih alanındaki değeri kontrol eder
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
    GetStudentsByClassroom();
    KTExamsAddExam.init();
});