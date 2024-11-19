let subjects = [];
let subtopics = [];
let questionAnswerChoices = [];
let questionDifficulties = [];
let answerTypeName = "Metin";
document.getElementById("questionAnswerChoices").value = JSON.stringify(questionAnswerChoices);



if ($("#QuestionType").val()) {
    questionAnswerChoices = JSON.parse(document.getElementById("questionAnswersListFromReload").value);
    createQuestionAnswersHtml($("#QuestionType").val());
}

//Question type change event.
async function onQuestionTypeChange() {
    let questionType = document.getElementById("QuestionType").value;

    if (questionType == 1) {
        questionAnswerChoices = [{ Answer: "", IsRightAnswer: false }, { Answer: "", IsRightAnswer: false }, { Answer: "", IsRightAnswer: false }, { Answer: "", IsRightAnswer: false }]
    }
    else if (questionType == 2) {
        questionAnswerChoices = [{ Answer: "", IsRightAnswer: false }, { Answer: "", IsRightAnswer: false }];
    }
    else {
        questionAnswerChoices = [{ Answer: "", IsRightAnswer: false }];
    }

    createQuestionAnswersHtml(questionType);
};

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
                document.getElementById("questionAnswersDiv").innerHTML = await getAlgorithmQuestionAnswer(questionAnswerChoices[0]);
                document.getElementById("questionAnswersDiv").removeAttribute("hidden");
                break;

            }
        case '3':
            document.getElementById("questionAnswersDiv").innerHTML = await getClassicQuestionAnswer();


            document.getElementById("questionAnswersDiv").removeAttribute("hidden");
            break;
        default:
            document.getElementById("questionAnswersDiv").setAttribute("hidden", true);
            break;
    }
}
function getCheckbox(getquestionAnswerChoices) {

    return `<label class="col-sm-12 col-form-label col-form-label-lg" for="QuestionAnswers">${localizer.answers}</label>
                <div class="form-check form-check-inline" id="choices">
                    <div class="form-check form-check-inline">
                        <input class="form-check-input" type="radio" ${getquestionAnswerChoices[0].IsRightAnswer ? 'checked' : ''}  name="answerOptions" id="choice0"  onChange="updateCheckedAnswers()">
                        <label class="form-check-label" for="true">True</label>
                    </div>
                    <div class="form-check form-check-inline">
                        <input class="form-check-input" type="radio" name="answerOptions" id="choice1" ${getquestionAnswerChoices[1].IsRightAnswer ? 'checked' : ''}  onChange="updateCheckedAnswers()">
                        <label class="form-check-label" for="false">False</label>
                    </div>
                </div>`
}

async function getClassicQuestionAnswer() {
let classicAnswerHtml = `<label class="col-sm-12 col-form-label col-form-label-lg" for="QuestionAnswers">Yanıt </label>`;

classicAnswerHtml += questionAnswerChoices.map((item, index) => {
    return `
        <div class="row g-3 flex-center justify-content-center">
            <div class="col-sm-9 col-12">
                <textarea name="questionAnswers" id="classicQuestionAnswer${index}" rows="2" 
                          class="form-control form-control-lg form-control-solid for="QuestionAnswers" 
                          oninput="updateQuestionAnswerChoices(this.value, ${index})"
                          placeholder="Yanıt ${index + 1}" 
                          style="margin-bottom: 5px; resize: none; overflow: hidden; font-size: 1em;">${item.Answer}</textarea>
            </div>
            <div class="col-sm-1 col-2 offset-4 offset-sm-0">
                <button class="btn btn-danger btn-sm" type="button" onclick="removeChoice(${index},'text')"> X </button>
            </div>
        </div>
    `;
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



async function getAlgorithmQuestionAnswer(Answer) {
    return `<label class="col-sm-12 col-form-label col-form-label-lg" for="QuestionAnswers">Yanıt</label>
            <textarea name="questionAnswers" id="algorithmQuestionAnswer" rows="4" 
                           class="form-control form-control-lg form-control-solid for="QuestionAnswers"
                           oninput="updateAlgorithmQuestionAnswerChoices(this)"
                           style="resize: none; overflow: hidden; font-size: 1em;">${Answer.Answer}</textarea>`;
}


function updateAlgorithmQuestionAnswerChoices(element) {
    questionAnswerChoices = [];
    questionAnswerChoices.push({ Answer: element.value, IsRightAnswer: true });
    document.getElementById("questionAnswerChoices").value = JSON.stringify(questionAnswerChoices);

    element.style.height = "auto";
    element.style.height = (element.scrollHeight) + "px";
}


async function getAnswerChoicesHtml(choiceType) {
    let answerChoicesHtml = `<div class="row">
        <label class="col-sm-6 col-form-label col-form-label-lg" for="QuestionAnswers">${localizer.answers}</label>
        <div class="col-sm-6 col-form-label col-form-label-lg" style="display:flex; justify-content:end;">
            <input class="form-check-input" type="radio" style="margin-right:5px;" name="answerType" id="stringRadio" ${(answerTypeName === "Metin" || answerTypeName === "Text") ? 'checked' : ''} onchange="updateAnswerType()">
            <label class="form-check-label" style="margin-right:5px;" for="stringRadio">${localizer.text}</label>
            <input class="form-check-input" type="radio" style="margin-right:5px;" name="answerType" id="imageRadio" ${(answerTypeName === "Resim" || answerTypeName === "Image") ? 'checked' : ''} onchange="updateAnswerType()">
            <label class="form-check-label" style="margin-right:5px;" for="imageRadio">${localizer.image}</label>
        </div>
    </div>`;

    if (answerTypeName === "Metin" || answerTypeName === localizer.text) {
        answerChoicesHtml += questionAnswerChoices.map((item, index) => {
            return `<div class="form-check mb-3" id="choices">
                <div class="row g-3 flex-center justify-content-center">
                    <div class="form-check col-sm-1 col-1 offset-xs-5 offset-sm-0">
                        <input class="form-check-input" type="${choiceType}" name="answerOptions" id="choice${index}" ${item.IsRightAnswer ? 'checked' : ''} onChange="updateCheckedAnswers()">
                    </div>
                    <div class="col-sm-9 col-12">
                       <textarea type="text" class="form-control form-control-solid" id="answerText${index}" placeholder="${localizer.newoption}" 
                                 onChange="updateAnswerText(${index})"
                                 oninput="adjustTextAreaHeight(this)" 
                                 style="resize: none; overflow: hidden; font-size: 1em;">${item.Answer}</textarea>
                    </div>
                    <div class="col-sm-1 col-2 offset-4 offset-sm-0">
                        <button class="btn btn-danger btn-sm" type="button" onclick="removeChoice(${index},'${choiceType}')"> X </button>
                    </div>
                </div>
            </div>`;
        }).join("");
        answerChoicesHtml += `<button class="btn btn-primary btn-sm col-6 offset-3" type="button" onclick="addNewChoice('${choiceType}')">${localizer.addnewoption}</button>`;
    } else if (answerTypeName === localizer.image) {
        answerChoicesHtml += questionAnswerChoices.map((item, index) => {
            return `<div class="form-check mb-3" id="choices">
                <div class="row g-3 flex-center justify-content-center">
                    <div class="form-check col-sm-1 col-1 offset-xs-5 offset-sm-0">
                        <input class="form-check-input" type="${choiceType}" name="answerOptions" id="choice${index}" ${item.IsRightAnswer ? 'checked' : ''} onChange="updateCheckedAnswers()">
                    </div>
                    <div class="col-sm-9 col-12">
                        <div class="row">
                            <div class="col-sm-12" id="answerImageDiv${index}">
                                 <input  type="file" class="form-control form-control-lg form-control-solid" id="file-input-${index}" accept=".png,.jpeg" onClick="setupFileInput(${index})" />
                            </div>                 
                            <div class="col-sm-3">
                            <div id="image-preview-${index}"></div>              
                            </div>  
                        </div>                              
                    </div>
                    <div class="col-sm-1 col-2 offset-4 offset-sm-0">
                        <button class="btn btn-danger btn-sm" type="button" onclick="removeChoice(${index},'${choiceType}')"> X </button>
                    </div>
                </div>
            </div>`
        }).join("");
        answerChoicesHtml += `<button class="btn btn-primary btn-sm col-6 offset-3" type="button" onclick="addNewChoice('${choiceType}')">${localizer.addnewoption}</button>`;
    }

    return answerChoicesHtml;
}

function setupFileInput(index) {
    const fileInput = document.getElementById(`file-input-${index}`);
    const imagePreview = document.getElementById(`image-preview-${index}`);
    const imageDiv = document.getElementById(`answerImageDiv${index}`);
    fileInput.addEventListener('change', function () {
        const file = this.files[0];
        if (file) {
            const reader = new FileReader();
            reader.onload = function (e) {
                const imageUrl = e.target.result;
                imageDiv.className = "col-sm-9 d-flex align-items-center";
                imagePreview.innerHTML = `<img class="img-fluid img-thumbnail" style="max-height:100px;" src="${imageUrl}" alt="Uploaded Image">`;
                questionAnswerChoices[index].Answer = imageUrl;
                questionAnswerChoices[index].IsImageAnswer = true;
                document.getElementById("questionAnswerChoices").value = JSON.stringify(questionAnswerChoices);
            };
            reader.readAsDataURL(file);
        } else {
            imagePreview.innerHTML = ""
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
            questionAnswerChoices.forEach((questionAnswerChoice, index) => {
                questionAnswerChoice.Answer = "";
                questionAnswerChoice.IsAnswerImage = false;
            })
        }
    }
    document.getElementById("questionAnswersDiv").innerHTML = await getAnswerChoicesHtml("radio");
}

async function addNewChoice(choiceType) {
    if (choiceType === "radio") {
        questionAnswerChoices.push({ Answer: "", IsRightAnswer: false });
        document.getElementById("questionAnswerChoices").value = JSON.stringify(questionAnswerChoices);
        document.getElementById("questionAnswersDiv").innerHTML = await getAnswerChoicesHtml(choiceType);
        questionAnswerChoices.forEach((item, index) => {
            document.getElementById(`answerText${index}`).placeholder = localizer.newoption;
        });
    } else if (choiceType === "classic" || choiceType === "text") {
        questionAnswerChoices.push({ Answer: "", IsRightAnswer: false });
        document.getElementById("questionAnswerChoices").value = JSON.stringify(questionAnswerChoices);
        document.getElementById("questionAnswersDiv").innerHTML = await getClassicQuestionAnswer();
        questionAnswerChoices.forEach((item, index) => {
            document.getElementById(`classicQuestionAnswer${index}`).placeholder = `Yanıt ${index + 1}`;
        });
    }
}


async function removeChoice(index, choiceType) {
    questionAnswerChoices.splice(index, 1);


    if (choiceType === "radio") {
        document.getElementById("questionAnswerChoices").value = JSON.stringify(questionAnswerChoices);
        document.getElementById("questionAnswersDiv").innerHTML = await getAnswerChoicesHtml(choiceType);
    } else if (choiceType === "classic" || choiceType === "text") {
        document.getElementById("questionAnswerChoices").value = JSON.stringify(questionAnswerChoices);
        document.getElementById("questionAnswersDiv").innerHTML = await getClassicQuestionAnswer();
    }

}

function removeChooseOption(elementId) {
    var selectElement = document.getElementById(elementId);
    var chooseOption = selectElement.querySelector('option[value=""]');

    if (chooseOption) {
        selectElement.removeChild(chooseOption);
    }
}

src = "~/lib/limonte-sweetalert2/sweetalert2.all.js";


function validateAndSubmitForm() {
    if (validateCheckBoxes()) {
        document.getElementById("candidateQuestionForm").submit();
    } 
}

function validateCheckBoxes() {
    let questionContent = document.getElementById("Content").value.trim();
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

    let questionType = $("#QuestionType").val();
    if (questionType === "1") {
        // Soru tipi 1 için doğrulama
        let questionAnswerChoices = JSON.parse(document.getElementById("questionAnswerChoices").value);
        let hasEmptyAnswer = questionAnswerChoices.some(choice => choice.Answer.trim() === "");
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
        } else {
            return true;
        }
    } else if (questionType === "2") {
        // Soru tipi 2 için doğrulama
        let questionAnswerChoices = JSON.parse(document.getElementById("questionAnswerChoices").value);
        let hasEmptyAnswer = questionAnswerChoices.some(choice => choice.Answer.trim() === "");
        if (hasEmptyAnswer) {
            Swal.fire({
                title: validationMessages.missingDataErrorTitle,
                text: validationMessages.addAnswerErrorText,
                icon: 'error'
            });
            return false;
        } else {
            return true;
        }
    } else if (questionType === "3") {
        // Soru tipi 3 için doğrulama
        let questionAnswerChoices = JSON.parse(document.getElementById("questionAnswerChoices").value);
        let hasEmptyAnswer = questionAnswerChoices.some(choice => choice.Answer.trim() === "");
        if (hasEmptyAnswer) {
            Swal.fire({
                title: validationMessages.missingDataErrorTitle,
                text: validationMessages.addAnswerErrorText,
                icon: 'error'
            });
            return false;
        } else {
            return true;
        }
    }
}