let subjects = [];
let subtopics = [];
let questionAnswerChoices = [];
let questionDifficulties = [];
let answerTypeName = "Metin";
let initialQuestionAnswerChoices = JSON.parse(document.getElementById("questionAnswerChoices").value);
document.getElementById("questionAnswerChoices").value = JSON.stringify(initialQuestionAnswerChoices);
src = "~/lib/limonte-sweetalert2/sweetalert2.all.js";


// Soru Tipi Değiştirildiğinde Soru Tipine Ugun Yanıt Alanı Getirmek için
async function onQuestionTypeChange() {
    var questionTypeSelect = document.getElementById('CandidateQuestionType').value;

    if (questionTypeSelect == 1) {
        questionAnswerChoices = [{ Answer: "", IsRightAnswer: false }, { Answer: "", IsRightAnswer: false }, { Answer: "", IsRightAnswer: false }, { Answer: "", IsRightAnswer: false }]
    }
    else if (questionTypeSelect == 2) {
        questionAnswerChoices = [{ Answer: "", IsRightAnswer: false }, { Answer: "", IsRightAnswer: false }];
    }
    else if (questionTypeSelect == 3) {
        questionAnswerChoices = [{ Answer: "", IsRightAnswer: false }];

    }
    createQuestionAnswersHtml(questionTypeSelect);
};

// Yanıt Alanını Oluşturmak İçin
async function createQuestionAnswersHtml(questionType) {
    switch (questionType) {
        case '1':
            {
                document.getElementById("questionAnswersDiv").innerHTML = await getAnswerChoicesHtml("radio");
                document.getElementById("questionAnswerChoices").value = JSON.stringify(questionAnswerChoices);
                document.getElementById("questionAnswersDiv").removeAttribute("hidden");
                break;
            }
        case '2':
            {
                document.getElementById("questionAnswersDiv").innerHTML = await getAlgorithmUpdateQuestionAnswer();
                document.getElementById("questionAnswersDiv").removeAttribute("hidden");
                break;

            }
        case '3':
            document.getElementById("questionAnswersDiv").innerHTML = await getClassicUpdateQuestionAnswer();
            document.getElementById("questionAnswersDiv").removeAttribute("hidden");
            break;
        default:
            document.getElementById("questionAnswersDiv").setAttribute("hidden", true);
            break;
    }
}

// Mevcut Yanıtları Göstermek İçin 
// Güncellenecek Algoritma Sorusu Cevapları İçin
function getAlgorithmUpdateQuestionAnswer(questionAnswerText) {
    return `<label class="col-sm-12 col-form-label col-form-label-lg" for="QuestionAnswers">Yanıt</label>
            <textarea name="QuestionAnswers" id="classicQuestionAnswer" rows="4" 
                      class="form-control form-control-lg form-control-solid for="QuestionAnswers" 
                      oninput="updateAlgorithmQuestionAnswerChoices(this)" 
                      style="font-size: 1em;">${questionAnswerText ? JSON.parse(questionAnswerText)[0].Answer : ""}</textarea>`;
}

function updateAlgorithmQuestionAnswerChoices(textarea) {
    questionAnswerChoices = [];
    questionAnswerChoices.push({ Answer: textarea.value, IsRightAnswer: true });
    document.getElementById("questionAnswerChoices").value = JSON.stringify(questionAnswerChoices);

    textarea.style.height = "auto";
    textarea.style.height = (textarea.scrollHeight) + "px";
}

// Güncellenecek Klasik Soru Cevapları İçin
function getClassicUpdateQuestionAnswer(getCandidateQuestionAnswersList) {
    if (getCandidateQuestionAnswersList) {
        questionAnswerChoices = JSON.parse(getCandidateQuestionAnswersList);
    }

    let classicAnswerHtml = `<label class="col-sm-12 col-form-label col-form-label-lg" for="QuestionAnswers">Yanıt </label>`;

    classicAnswerHtml += questionAnswerChoices.map((item, index) => {
        return `<div class="row g-3 align-items-center flex-start justify-content-center">
                    <div class="col-sm-11 col-12">
                        <textarea name="questionAnswers" id="classicQuestionAnswer${index}" 
                                  class="form-control form-control-lg form-control-solid" 
                                  oninput="updateQuestionAnswerChoices(this.value, ${index})" 
                                  placeholder="Yanıt ${index + 1}"
                                  rows="4" 
                                  style="margin-bottom: 5px;">${item.Answer}</textarea>
                    </div>
                    <div class="col-sm-1 col-2 offset-4 offset-sm-0">
                        <button class="btn btn-danger btn-sm" type="button" onclick="removeChoice(${index},'text')"> X </button>
                    </div>
                </div>`;
    }).join("");

    classicAnswerHtml += `<button class="btn btn-primary btn-sm col-6 offset-3" type="button" onclick="addNewChoice('text')">Metin Cevap Ekle</button>`;

    return classicAnswerHtml;
}


function updateQuestionAnswerChoices(value, index) {

    questionAnswerChoices[index].Answer = value;
    questionAnswerChoices[index].IsRightAnswer = true;
    document.getElementById("questionAnswerChoices").value = JSON.stringify(questionAnswerChoices);

    let textarea = document.getElementById(`classicQuestionAnswer${index}`);
    textarea.style.height = "auto";
    textarea.style.height = (textarea.scrollHeight) + "px";
}


// Güncellenecek Test Sorusu Cevapları İçin
function getAnswerChoicesHtmlQuestion(choiceTypex, getquestionAnswerChoices) {
    var choiceTypex = choiceTypex === 1 ? "checkbox" : "radio";
    questionAnswerChoices = JSON.parse(getquestionAnswerChoices);
    let answerChoicesHtml = `<div class="row">
        <label class="col-sm-6 col-form-label col-form-label-lg" for="QuestionAnswers">${localizedStrings.answers}</label>
        <div class="col-sm-6 col-form-label col-form-label-lg" style="display:flex; justify-content:end;">
            <input class="form-check-input" style="margin-right:5px;" type="radio" name="answerType" id="stringRadio" ${questionAnswerChoices[0].IsImageAnswer === false ? 'checked' : ''} onchange="updateAnswerType()">
            <label class="form-check-label" style="margin-right:5px;" for="stringRadio">${localizedStrings.text}</label>
            <input class="form-check-input" type="radio" style="margin-right:5px;" name="answerType" id="imageRadio" ${questionAnswerChoices[0].IsImageAnswer === true ? 'checked' : ''} onchange="updateAnswerType()">
            <label class="form-check-label" style="margin-right:5px;" for="imageRadio">${localizedStrings.image}</label>
        </div>
    </div>`;

    if (questionAnswerChoices[0].IsImageAnswer === false) {
        answerChoicesHtml += questionAnswerChoices.map((item, index) => {
            return `<div class="form-check mb-3" id="choices">
                <div class="row g-3 flex-center justify-content-center">
                    <div class="form-check col-sm-1 col-1 offset-xs-5 offset-sm-0">
                        <input class="form-check-input" type="${choiceTypex}" name="answerOptions" id="choice${index}" ${item.IsRightAnswer ? 'checked' : ''} onChange="updateCheckedAnswers()">
                    </div>
                    <div class="col-sm-9 col-12">
                        <textarea type="text" class="form-control form-control-solid" id="answerText${index}" 
                                  placeholder="${localizedStrings.newoption}" 
                                  oninput="adjustTextAreaHeight(this)" 
                                  onChange="updateAnswerText(${index})"
                                  style="margin-bottom: 5px;">${item.Answer}</textarea>
                    </div>
                    <div class="col-sm-1 col-2 offset-4 offset-sm-0">
                        <button class="btn btn-danger btn-sm" type="button" onclick="removeChoice(${index},'${choiceTypex}')"> X </button>
                    </div>
                </div>
            </div>`;
        }).join("");
        answerChoicesHtml += `<button class="btn btn-primary btn-sm col-6 offset-3" type="button" onclick="addNewChoice('${choiceTypex}','${localizedStrings.text}')">${localizedStrings.addnewoption}</button>`;
    } else if (questionAnswerChoices[0].IsImageAnswer === true) {
        answerChoicesHtml += questionAnswerChoices.map((item, index) => {
            return `<div class="form-check mb-3" id="choices">
                <div class="row g-3 flex-center justify-content-center">
                    <div class="form-check col-sm-1 col-1 offset-xs-5 offset-sm-0">
                        <input class="form-check-input" type="${choiceTypex}" name="answerOptions" id="choice${index}" ${item.IsRightAnswer ? 'checked' : ''} onChange="updateCheckedAnswers()">
                    </div>
                    <div class="col-sm-9 col-12">
                        <div class="row">
                            <div class="col-sm-9 d-flex align-items-center" id="answerImageDiv${index}">
                                <input type="file" class="form-control form-control-lg form-control-solid" id="file-input-${index}" accept=".png,.jpeg,.jpg" onClick="setupFileInput(${index})" />
                            </div>
                            <div class="col-sm-3">
                                <div id="image-preview-${index}">
                                    <img class="img-fluid img-thumbnail" style="max-height:100px;" src="${item.Answer}" alt="Uploaded Image">
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-1 col-2 offset-4 offset-sm-0">
                        <button class="btn btn-danger btn-sm" type="button" onclick="removeChoice(${index},'${choiceTypex}')"> X </button>
                    </div>
                </div>
            </div>`;
        }).join("");

        answerChoicesHtml += `<button class="btn btn-primary btn-sm col-6 offset-3" type="button" onclick="addNewChoice('${choiceTypex}','${localizedStrings.image}')">${localizedStrings.addnewoption}</button>`;
    }

    return answerChoicesHtml;
}

async function getAnswerChoicesHtml(choiceType) {
    let answerChoicesHtml = `<div class="row">
        <label class="col-sm-6 col-form-label col-form-label-lg" for="QuestionAnswers">${localizedStrings.answers}</label>
        <div class="col-sm-6 col-form-label col-form-label-lg" style="display:flex; justify-content:end;">
           <input class="form-check-input" type="radio" style="margin-right:5px;" name="answerType" id="stringRadio" ${(answerTypeName === "Metin" || answerTypeName === localizedStrings.text) ? 'checked' : ''} onchange="updateAnswerType()">
            <label class="form-check-label" style="margin-right:5px;" for="stringRadio">${localizedStrings.text}</label>
            <input class="form-check-input" type="radio" style="margin-right:5px;" name="answerType" id="imageRadio" ${(answerTypeName === "Resim" || answerTypeName === localizedStrings.image) ? 'checked' : ''} onchange="updateAnswerType()">
            <label class="form-check-label" style="margin-right:5px;" for="imageRadio">${localizedStrings.image}</label>
        </div>
    </div>`;

    if (answerTypeName === localizedStrings.text || answerTypeName === "Metin") {
        answerChoicesHtml += questionAnswerChoices.map((item, index) => {
            return `<div class="form-check mb-3" id="choices">
                <div class="row g-3 flex-center justify-content-center">
                    <div class="form-check col-sm-1 col-1 offset-xs-5 offset-sm-0">
                        <input class="form-check-input" type="${choiceType}" name="answerOptions" id="choice${index}" ${item.IsRightAnswer ? 'checked' : ''} onChange="updateCheckedAnswers()">
                    </div>
                    <div class="col-sm-9 col-12">
                      <textarea type="text" class="form-control form-control-solid" id="answerText${index}" 
                                  placeholder="${localizedStrings.newoption}" 
                                  oninput="adjustTextAreaHeight(this)" 
                                  onChange="updateAnswerText(${index})"
                                  style="margin-bottom: 5px;">${item.Answer}</textarea>
                    </div>
                    <div class="col-sm-1 col-2 offset-4 offset-sm-0">
                        <button class="btn btn-danger btn-sm" type="button" onclick="removeChoice(${index},'${choiceType}')"> X </button>
                    </div>
                </div>
            </div>`;
        }).join("");
        answerChoicesHtml += `<button class="btn btn-primary btn-sm col-6 offset-3" type="button" onclick="addNewChoice('${choiceType}','${localizedStrings.text}')">${localizedStrings.addnewoption}</button>`;
    } else if (answerTypeName === localizedStrings.image) {
        answerChoicesHtml += questionAnswerChoices.map((item, index) => {
            return `<div class="form-check mb-3" id="choices">
                <div class="row g-3 flex-center justify-content-center">
                    <div class="form-check col-sm-1 col-1 offset-xs-5 offset-sm-0">
                        <input class="form-check-input" type="${choiceType}" name="answerOptions" id="choice${index}" ${item.IsRightAnswer ? 'checked' : ''} onChange="updateCheckedAnswers()">
                    </div>
                    <div class="col-sm-9 col-12">
                        <div class="row">
                            <div class="${item.Answer ? `col-sm-9` : `col-sm-12`}" id="answerImageDiv${index}">
                                 <input  type="file" class="form-control form-control-lg form-control-solid" id="file-input-${index}" accept=".png,.jpeg,.jpg" onClick="setupFileInput(${index})" />
                            </div>                 
                            <div class="col-sm-3">
                                <div id="image-preview-${index}">
                                   ${item.Answer ? ` <img class="img-fluid img-thumbnail" style="max-height:100px;" src="${item.Answer}" alt="Uploaded Image">` : ``}
                                </div>              
                            </div>  
                        </div>                              
                    </div>
                    <div class="col-sm-1 col-2 offset-4 offset-sm-0">
                        <button class="btn btn-danger btn-sm" type="button" onclick="removeChoice(${index},'${choiceType}')"> X </button>
                    </div>
                </div>
            </div>`
        }).join("");
        answerChoicesHtml += `<button class="btn btn-primary btn-sm col-6 offset-3" type="button" onclick="addNewChoice('${choiceType}','${localizedStrings.image}')">${localizedStrings.addnewoption}</button>`;
    }

    return answerChoicesHtml;
}


// setupFileInput fonksiyonunda varsayılan bir resmi önizleme alanına yerleştirme
function setupFileInput(index) {
    const fileInput = document.getElementById(`file-input-${index}`);
    const imagePreview = document.getElementById(`image-preview-${index}`);

    // Varsayılan resim URL'i
    const defaultImageUrl = "/images/blank-profile-picture.png";

    fileInput.addEventListener('change', function () {
        const file = this.files[0];
        if (file) {
            const reader = new FileReader();
            reader.onload = function (e) {
                const imageUrl = e.target.result;
                imagePreview.innerHTML = `<img class="img-fluid img-thumbnail" style="max-height:100px;" src="${imageUrl}" alt="Uploaded Image">`;
                questionAnswerChoices[index].Answer = imageUrl;
                questionAnswerChoices[index].IsImageAnswer = true;
                document.getElementById("questionAnswerChoices").value = JSON.stringify(questionAnswerChoices);
            };
            reader.readAsDataURL(file);
        } else {
            imagePreview.innerHTML = defaultImagePreviewHtml; // Dosya seçilmediğinde varsayılan resmi göster
        }
    });
}
function adjustTextAreaHeight(textarea) {
    textarea.style.height = "auto";
    textarea.style.height = (textarea.scrollHeight) + "px";
}

function updateAnswerText(index) {
    let textarea = document.getElementById(`answerText${index}`);
    questionAnswerChoices[index].Answer = textarea.value;
    document.getElementById("questionAnswerChoices").value = JSON.stringify(questionAnswerChoices);
    adjustTextAreaHeight(textarea);
}

function updateCheckedAnswers() {
    questionAnswerChoices.forEach((item, index) => {
        item.IsRightAnswer = document.getElementById(`choice${index}`).checked;
    });
    document.getElementById("questionAnswerChoices").value = JSON.stringify(questionAnswerChoices);
}


async function updateAnswerType() {
    const radioButtons = document.getElementsByName("answerType");
    for (const button of radioButtons) {
        if (button.checked) {
            answerTypeName = button.nextElementSibling.textContent.trim();

            const isImageAnswer = (answerTypeName === localizedStrings.image);

            questionAnswerChoices.forEach((questionAnswerChoice, index) => {
                questionAnswerChoice.Answer = "";
                questionAnswerChoice.IsImageAnswer = isImageAnswer;
            });
        }
    }
    document.getElementById("questionAnswersDiv").innerHTML = await getAnswerChoicesHtml("radio");
}

async function addNewChoice(choiceType, answerTypeName) {
    const defaultImagePreviewHtml = `<img class="img-fluid img-thumbnail" style="max-height:100px;" src="/images/blank-picture.png" alt="Default Image">`;

    if (choiceType === "radio") {
        questionAnswerChoices.push({ Answer: "", IsRightAnswer: false });
        document.getElementById("questionAnswerChoices").value = JSON.stringify(questionAnswerChoices);
        document.getElementById("questionAnswersDiv").innerHTML = await getAnswerChoicesHtmlQuestion(choiceType, JSON.stringify(questionAnswerChoices));
    } else if (choiceType === "classic" || choiceType === "text") {
        questionAnswerChoices.push({ Answer: "", IsRightAnswer: false });
        document.getElementById("questionAnswerChoices").value = JSON.stringify(questionAnswerChoices);
        document.getElementById("questionAnswersDiv").innerHTML = await getClassicUpdateQuestionAnswer();
    }

    const newIndex = questionAnswerChoices.length - 1;
    const imagePreview = document.getElementById(`image-preview-${newIndex}`);
    imagePreview.innerHTML = defaultImagePreviewHtml;
}

async function addChoice(choiceType, getquestionAnswerChoices) {
    getquestionAnswerChoices.push({ Answer: "", IsRightAnswer: false, Id: "", QuestionId: "" });
    document.getElementById("questionAnswerChoices").value = JSON.stringify(getquestionAnswerChoices);
    var buttonElement = document.getElementById("submitButton");
    document.getElementById("questionAnswersDiv").removeChild(buttonElement);
    document.getElementById("questionAnswersDiv").innerHTML += await getAnswerChoicesHtml(choiceType);

    getquestionAnswerChoices.forEach((item, index) => {
        document.getElementById(`answerText${index}`).placeholder = $localizedStrings.newoption;
    });
}

async function removeChoice(index, choiceType) {
    if (questionAnswerChoices[index].IsAnswerInUse) {
        Swal.fire({
            icon: 'error',
            title: 'Kaldırılamaz!',
            text: 'Bu seçenek kullanımda olduğu için kaldırılamaz.',
        });
        return;
    }
    questionAnswerChoices.splice(index, 1);
    if (questionAnswerChoices.length === 0) {
        questionAnswerChoices.push({ Answer: "", IsRightAnswer: false, IsImageAnswer: false });
    }
    if (choiceType === "radio") {
        document.getElementById("questionAnswerChoices").value = JSON.stringify(questionAnswerChoices);
        document.getElementById("questionAnswersDiv").innerHTML = await getAnswerChoicesHtmlQuestion(choiceType, JSON.stringify(questionAnswerChoices));
    } else if (choiceType === "classic" || choiceType === "text") {
        document.getElementById("questionAnswerChoices").value = JSON.stringify(questionAnswerChoices);
        document.getElementById("questionAnswersDiv").innerHTML = await getClassicUpdateQuestionAnswer();
    }
}

async function populateSelectList(selectListId, data) {
    let selectListOptions = data.map((item, index) => `<option value="${item.value}">${item.text}</option>`);
    let selectList = `<option value="" disabled="" selected="">${localizer.choose}</option>`.concat(selectListOptions);
    document.getElementById(selectListId).innerHTML = selectList;
}

function removeChooseOption(elementId) {
    var selectElement = document.getElementById(elementId);
    var chooseOption = selectElement.querySelector('option[value=""]');

    if (chooseOption) {
        selectElement.removeChild(chooseOption);
    }
}



function validateAndSubmitFormUpdate() {
    const updateElement = document.getElementById('kt_modal_update_question');
    const updateForm = document.getElementById('kt_modal_update_question_form');
    const updateModal = new bootstrap.Modal(updateElement);
    const submitButton = updateElement.querySelector('[data-kt-question-modal-action="submit"]');
    if (validateCheckBoxes()) {
        submitButton.setAttribute('data-kt-indicator', 'on');
        submitButton.disabled = true;
        setTimeout(function () {
            submitButton.removeAttribute('data-kt-indicator');
            submitButton.disabled = false;
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
                    updateModal.hide();
                }
            });
        }, 2000);
        updateForm.submit();
    }
}

function validateAnswerChoices(questionAnswerChoices) {
    const uniqueChoices = new Set();

    for (let i = 0; i < questionAnswerChoices.length; i++) {
        const choice = questionAnswerChoices[i].Answer.trim();

        if (uniqueChoices.has(choice)) {

            return false;
        }

        uniqueChoices.add(choice);
    }
    return true;
}

function validateCheckBoxes() {
    let questionContent = document.getElementById("candidateQuestionUpdateContent").value.trim();
    if (questionContent === "") {
        Swal.fire({
            title: validationMessages.missingDataErrorTitle,
            text: validationMessages.pleaseProvideQuestionContent,
            icon: 'error'
        });
        return false;
    }

    let input = document.getElementById('fileInput');
    if (input.files.length > 0) {
        let file = input.files[0];
        let fileSizeInMB = file.size / (1024 * 1024);
        if (fileSizeInMB > 1) {
            Swal.fire({
                title: validationMessages.imageFileSizeErrorTitle,
                text: validationMessages.imageFileSizeErrorText,
                icon: 'error'
            });
            return false;
        }
    }

    let questionType = $("#CandidateQuestionType").val();
    let questionAnswerChoices = JSON.parse(document.getElementById("questionAnswerChoices").value);
    let hasEmptyAnswer = questionAnswerChoices.some(choice => choice.Answer.trim() === "");
    if (questionType === "1") {
        // Soru tipi 1 için doğrulama
        if (hasEmptyAnswer) {
            Swal.fire({
                title: validationMessages.missingDataErrorTitle,
                text: validationMessages.addAnswerErrorText,
                icon: 'error'
            });
            return false;
        } else if (questionAnswerChoices.length < 2) {
            Swal.fire({
                title: validationMessages.missingDataErrorTitle,
                text: validationMessages.minAnswersErrorText,
                icon: 'error'
            });
            return false;
        } else if ($("input[name='answerOptions']:checked").length === 0) {
            Swal.fire({
                title: validationMessages.missingDataErrorTitle,
                text: validationMessages.selectCorrectAnswerErrorText,
                icon: 'error'
            });
            return false;
        }
    } else if (questionType === "2" || questionType === "3") {
        // Soru tipi 2 ve Soru tipi 3 için doğrulama
        if (hasEmptyAnswer) {
            Swal.fire({
                title: validationMessages.missingDataErrorTitle,
                text: validationMessages.addAnswerErrorText,
                icon: 'error'
            });
            return false;
        }
    }

    if (!validateAnswerChoices(questionAnswerChoices)) {
        Swal.fire({
            title: validationMessages.missingDataErrorTitle,
            text: "Bu yanıtın aynısından mevcut",
            icon: 'error'
        });

        return false
    }

    return true;
}

