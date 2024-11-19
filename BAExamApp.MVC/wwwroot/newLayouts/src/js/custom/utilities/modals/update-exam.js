"use strict";
let selectElement = null;

let exam = null;
// Class definition
var KTModalNewTarget = function () {
    var submitButton;
    var cancelButton;
    var validator;
    var form;
    var modal;
    var modalEl;

    // Init form inputs
    var initForm = function () {
        // Tags. For more info, please visit the official plugin site: https://yaireo.github.io/tagify/
        //var tags = new Tagify(form.querySelector('[name="tags"]'), {
        //    whitelist: ["Important", "Urgent", "High", "Medium", "Low"],
        //    maxTags: 5,
        //    dropdown: {
        //        maxItems: 10,           // <- mixumum allowed rendered suggestions
        //        enabled: 0,             // <- show suggestions on focus
        //        closeOnSelect: false    // <- do not hide the suggestions dropdown once an item has been selected
        //    }
        //});
        //tags.on("change", function () {
        //    // Revalidate the field when an option is chosen
        //    validator.revalidateField('tags');
        //});

        // Due date. For more info, please visit the official plugin site: https://flatpickr.js.org/
        var dueDate = $(form.querySelector('[name="due_date"]'));
        dueDate.flatpickr({
            enableTime: true,
            dateFormat: "d, M Y, H:i",
        });

        // Team assign. For more info, plase visit the official plugin site: https://select2.org/
        $(form.querySelector('[name="team_assign"]')).on('change', function () {
            // Revalidate the field when an option is chosen
            validator.revalidateField('team_assign');
        });
    }

    // Handle form validation and submittion
    var handleForm = function () {
        // Stepper custom navigation

        // Init form validation rules. For more info check the FormValidation plugin's official documentation:https://formvalidation.io/
        validator = FormValidation.formValidation(
            form,
            {
                fields: {
                    exam_date: {
                        validators: {
                            notEmpty: {
                                message: 'Sınav tarih alanı boş bırakılamaz'
                            }
                        }
                    },
                    exam_type: {
                        validators: {
                            notEmpty: {
                                message: 'Sınav tipi alanı boş bırakılamaz'
                            }
                        }
                    },
                    exam_rule: {
                        validators: {
                            notEmpty: {
                                message: 'Bu alan boş bırakılamaz. Eğer uygun bir sınav kuralı yoksa önce sınav kuralı ekleyiniz.'
                            }
                        }
                    },
                    exam_creation_type: {
                        validators: {
                            notEmpty: {
                                message: 'Sınav Oluşturma Türü alanı boş bırakılamaz'
                            }
                        }
                    },
                    exam_classrooms: {
                        validators: {
                            notEmpty: {
                                message: 'Lütfen bir sınıf seçiniz'
                            }
                        }
                    },
                    TrainerExplanation: {
                        validators: {
                            notEmpty: {
                                message: 'Lütfen sınav güncelleme açıklamasını giriniz'
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

        // Action buttons
        submitButton.addEventListener('click', function (e) {
            e.preventDefault();

            // Validate form before submit
            if (validator) {
                validator.validate().then(function (status) {
                    console.log('validated!');

                    if (status == 'Valid') {
                        submitButton.setAttribute('data-kt-indicator', 'on');

                        // Disable button to avoid multiple click 
                        submitButton.disabled = true;

                        setTimeout(function () {
                            submitButton.removeAttribute('data-kt-indicator');

                            // Enable button
                            submitButton.disabled = false;

                            // Show success message. For more info check the plugin's official documentation: https://sweetalert2.github.io/
                            Swal.fire({
                                text: "Form has been successfully submitted!",
                                icon: "success",
                                buttonsStyling: false,
                                confirmButtonText: "Ok, got it!",
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
                    } else {
                        // Show error message.
                        //Swal.fire({
                        //    text: "Sorry, looks like there are some errors detected, please try again.",
                        //    icon: "error",
                        //    buttonsStyling: false,
                        //    confirmButtonText: "Ok, got it!",
                        //    customClass: {
                        //        confirmButton: "btn btn-primary"
                        //    }
                        //});
                    }
                });
            }
        });

        cancelButton.addEventListener('click', function (e) {
            e.preventDefault();

            Swal.fire({
                text: "Are you sure you would like to cancel?",
                icon: "warning",
                showCancelButton: true,
                buttonsStyling: false,
                confirmButtonText: "Yes, cancel it!",
                cancelButtonText: "No, return",
                customClass: {
                    confirmButton: "btn btn-primary",
                    cancelButton: "btn btn-active-light"
                }
            }).then(function (result) {
                if (result.value) {
                    form.reset(); // Reset form	
                    modal.hide(); // Hide modal				
                } else if (result.dismiss === 'cancel') {
                    Swal.fire({
                        text: "Your form has not been cancelled!.",
                        icon: "error",
                        buttonsStyling: false,
                        confirmButtonText: "Ok, got it!",
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
            // Elements
            modalEl = document.querySelector('#kt_modal_new_target');

            if (!modalEl) {
                return;
            }

            modal = new bootstrap.Modal(modalEl);

            form = document.querySelector('#kt_modal_new_target_form');
            submitButton = document.getElementById('kt_modal_new_target_submit');
            cancelButton = document.getElementById('kt_modal_new_target_cancel');

            initForm();
            handleForm();
        }
    };
}();

async function getExamData(examId) {
    return $.ajax({
        url: "/trainer/exam/getexam",
        data: { examId: examId },
    });
}




async function loadExamData(examId) {
    const form = document.getElementById('kt_modal_new_target_form');
    exam = await getExamData(examId);


    console.log(exam);

    form.elements["Id"].value = exam.id;
    form.elements["Name"].value = exam.name;

    form.elements["ExamDateTime"].value = exam.examDateTime;
    form.elements["ExamDuration"].value = exam.examDuration;

    form.elements["ExamType"].value = exam.examType;
    form.elements["ExamCreationType"].value = exam.examCreationType;

    form.elements["ExamRuleId"].value = exam.examRuleId;
    form.elements["Description"].value = exam.description;
    form.elements["MaxScore"].value = exam.maxScore;
    form.elements["BonusScore"].value = exam.bonusScore;
    form.elements["ExamClassroomsIds"].value = exam.examClassroomsIds;
    form.elements["StudentIds"].value = exam.examClassroomsIds;
    form.elements["TrainerExplanation"].value = exam.trainerExplanation;
    form.elements["forClassroom"].value = exam.examClassroomsIds;
}

//$("#ExamDateTime").flatpickr({
//    onReady: function () {
//        this.set("minDate", "today");
//        this.jumpToDate(new Date(Date.now()));
//    },
//    dateFormat: "d-m-Y",
//    locale: "tr",
//});


$("#ExamDuration").flatpickr({
    enableTime: true,
    noCalendar: true,
    dateFormat: "H:i",
    time_24hr: true,
});

$(document).ready(function () {
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

    var switchElement = $("#ogrenci");
    var studentArea = $("#studentArea");
    var ogrenciLabel = $("#ogrenciLabel");
    var sinifLabel = $("#sinifLabel");

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
});




// On document ready
KTUtil.onDOMContentLoaded(function () {
    KTModalNewTarget.init();
});