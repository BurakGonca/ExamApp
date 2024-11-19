//SelectLists
let subjects = [];
let subtopics = [];
let examRuleSubjects = [];
let examRuleSubjectsTable = [];
let questionDifficulties = [];
let questionTypes = [];

//Ajax functions for getting selectList
async function getSubjects(selectedProductId) {

    return $.ajax({
        url: '/Admin/ExamRule/GetSubjectsByProductId',
        data: { productId: selectedProductId },
    });
}
async function getSubtopics(selectedSubjectId) {

    return $.ajax({
        url: '/Admin/ExamRule/GetSubtopicsBySubjectId',
        data: { subjectId: selectedSubjectId },
    });
}

async function getQuestionTypes(selectedExamTypeId) {

    return $.ajax({
        url: '/Admin/ExamRule/GetQuestionTypes',
        data: { examTypeId: selectedExamTypeId }
    });
}

async function getQuestionDifficulties() {

    return $.ajax({
        url: '/Admin/ExamRule/GetQuestionDifficulties'
    });
}

async function getQuestionListBySubtopic(selectedSubtopicId) {
    return $.ajax({
        url: "/Admin/ExamRule/GetQuestionListBySubtopic",
        data: { subtopicId: selectedSubtopicId },
    });
}

//Product type change event.
async function onProductChange() {
    subjects = subjects ? await getSubjects($("#ProductId").val()) : subjects;
    populateSelectList("SubjectId", subjects);

    examRuleSubjects = [];
    examRuleSubjectsTable = [];

    updateTable();
    document.getElementById("examRuleSubjects").value = JSON.stringify(examRuleSubjects);
};

//Subject type change event.
async function onSubjectChange() {
    subtopics = subtopics ? await getSubtopics($("#SubjectId").val()) : subtopics;
    populateSelectList("SubtopicId", subtopics);
};

//Exam type change event.
async function onExamTypeChange() {
    questionTypes = questionTypes ? await getQuestionTypes($("#ExamType").val()) : questionTypes;
    populateSelectList("QuestionType", questionTypes);
    questionTypes = [];

    questionDifficulties = questionDifficulties ? await getQuestionDifficulties() : questionDifficulties;
    populateSelectList("QuestionDifficultyId", questionDifficulties);
    questionDifficulties = [];
};



// Subtopic change event
async function onSubtopicChange() {
    let subtopicId = $("#SubtopicId").val();
    let questionList = await getQuestionListBySubtopic(subtopicId);

    let questionInformations = [];
    let countMap = {};

    questionList.forEach(question => {
        let key = `${question.questionType}-${question.questionDifficultyName}`;
        if (countMap[key]) {
            countMap[key].count += 1;
        } else {
            countMap[key] = {
                questionType: question.questionType,
                questionDifficultyName: question.questionDifficultyName,
                count: 1
            };
        }
    });

    Object.values(countMap).forEach(item => {
        questionInformations.push({
            questionType: item.questionType,
            questionDifficultyName: item.questionDifficultyName,
            questionCount: item.count
        });
    });

    document.querySelector('#questionTableContainer').style.display = 'block';

    let target = document.querySelector("#questionTableContent");

    questionTypes = questionTypes ? await getQuestionTypes($("#ExamType").val()) : questionTypes;

    target.textContent = "";

    if (questionInformations && questionInformations.length > 0) {
        questionInformations.forEach(question => {
            let rowItem = document.createElement("tr");

            let typeCell = document.createElement("td");
            typeCell.classList.add("text-center");
            let questionTypeStr = question.questionType.toString();
            let matchedType = questionTypes.find(ques => ques.value === questionTypeStr);

            typeCell.textContent = matchedType ? matchedType.text : question.questionType;
            rowItem.appendChild(typeCell);

            let difficultyCell = document.createElement("td");
            difficultyCell.classList.add("text-center");
            difficultyCell.textContent = question.questionDifficultyName;
            rowItem.appendChild(difficultyCell);

            let countCell = document.createElement("td");
            countCell.classList.add("text-center");
            countCell.textContent = question.questionCount;
            rowItem.appendChild(countCell);

            target.appendChild(rowItem);
        });
    } else {
        let emptyMessage = document.createElement("tr");
        let cell = document.createElement("td");
        cell.classList.add("text-center");
        cell.colSpan = 3;
        cell.textContent = "No questions available for this subtopic./Bu alt konuda soru bulunmamaktadır.";
        emptyMessage.appendChild(cell);
        target.appendChild(emptyMessage);
    }
    questionList = [];
};


//Add new rule
async function addNewRule() {
    if (document.getElementById("Name").value && document.getElementById("ProductId").value && document.getElementById("SubjectId").value && document.getElementById("SubtopicId").value && document.getElementById("ExamType").value && document.getElementById("QuestionType").value && document.getElementById("QuestionDifficultyId").value && document.getElementById("QuestionCount").value && document.getElementById("Description").value) {

        document.getElementById("inputValidation").setAttribute("hidden", true);
        document.getElementById("examTypeValidation").setAttribute("hidden", true);
        document.getElementById("sufficientQuestionValidation").setAttribute("hidden", true);
        document.getElementById("numberQuestionValidation").setAttribute("hidden", true);


        let subtopicId = document.getElementById("SubtopicId").value;
        let questionList = await getQuestionListBySubtopic(subtopicId);

        let questionType = parseInt(document.getElementById("QuestionType").value);
        let questionDifficultyId = document.getElementById("QuestionDifficultyId").value;
        let questionCount = parseInt(document.getElementById("QuestionCount").value);

        let difficultyMap = {
            "8121e12c-38bc-4fd1-4239-08dce2038902": "Çok Kolay",
            "e1fdd58a-9497-4a64-4238-08dce2038902": "Kolay",
            "0e3cb392-d446-4ee7-4237-08dce2038902": "Orta",
            "82fb50ae-596d-42fe-4236-08dce2038902": "Zor",
            "cc8ce567-d4ef-4d1e-4235-08dce2038902": "Çok Zor"
        };

        let matchedQuestions = questionList.filter(question => {
            return parseInt(question.questionType) === questionType
                && difficultyMap[questionDifficultyId] === question.questionDifficultyName;
        });

        if (1 > questionCount) {
            document.getElementById("numberQuestionValidation").removeAttribute("hidden");
            return;
        }

        if (matchedQuestions.length < questionCount) {
            document.getElementById("sufficientQuestionValidation").removeAttribute("hidden");
            return;
        }

        let examRuleSubjectVM = {
            SubjectId: document.getElementById("SubjectId").value,
            SubtopicId: document.getElementById("SubtopicId").value,
            ExamType: parseInt(document.getElementById("ExamType").value),
            QuestionType: questionType,
            QuestionDifficultyId: questionDifficultyId,
            QuestionCount: questionCount,
        };

        let hasSameExamType = examRuleSubjects.some((rule) => {
            return (rule.ExamType === examRuleSubjectVM.ExamType);
        });

        if ((hasSameExamType && examRuleSubjects.length > 0) || examRuleSubjects.length === 0) {
            let hasSameRule = examRuleSubjects.some((rule) => {
                return (rule.SubjectId === examRuleSubjectVM.SubjectId
                    && rule.SubtopicId === examRuleSubjectVM.SubtopicId
                    && rule.ExamType === examRuleSubjectVM.ExamType
                    && rule.QuestionType === examRuleSubjectVM.QuestionType
                    && rule.QuestionDifficultyId === examRuleSubjectVM.QuestionDifficultyId);
            });

            if (!hasSameRule) {
                document.getElementById("sameRuleValidation").setAttribute("hidden", true);
                examRuleSubjects.push(examRuleSubjectVM);

                examRuleSubjectsTable.push({
                    SubjectName: document.getElementById("SubjectId").options[document.getElementById("SubjectId").selectedIndex].text,
                    SubtopicName: document.getElementById("SubtopicId").options[document.getElementById("SubtopicId").selectedIndex].text,
                    ExamType: document.getElementById("ExamType").options[document.getElementById("ExamType").selectedIndex].text,
                    QuestionType: document.getElementById("QuestionType").options[document.getElementById("QuestionType").selectedIndex].text,
                    QuestionDifficulty: document.getElementById("QuestionDifficultyId").options[document.getElementById("QuestionDifficultyId").selectedIndex].text,
                    QuestionCount: questionCount
                });
            } else {
                document.getElementById("sameRuleValidation").removeAttribute("hidden");
            }
            refreshModal();
        } else {
            document.getElementById("examTypeValidation").removeAttribute("hidden");
        }

    } else {
        document.getElementById("inputValidation").removeAttribute("hidden");
    };
}



async function deleteRule(index) {
    examRuleSubjects.splice(index, 1);
    examRuleSubjectsTable.splice(index, 1);
    refreshModal();
}

//Function for populating select lists
async function populateSelectList(selectListId, data) {
    let selectListOptions = data.map((item, index) => `<option value="${item.value}">${item.text}</option>`);
    let selectList = `<option value="" disabled selected>--- Seçiniz ---</option>`.concat(selectListOptions);
    document.getElementById(selectListId).innerHTML = selectList;
}

async function refreshModal() {
    updateTable();
    clearExamRuleInputs();
    document.getElementById("examRuleSubjects").value = JSON.stringify(examRuleSubjects);
    updateTotalQuestionCount();  // Toplam QuestionCount hesaplaması
}

// Toplam Question Count hesaplama fonksiyonu
function updateTotalQuestionCount() {
    const totalCountElement = document.getElementById("totalCount");
    const totalQuestions = examRuleSubjects.reduce((total, rule) => total + rule.QuestionCount, 0);
    totalCountElement.textContent = totalQuestions;
}


//Update table
async function updateTable() {
    document.getElementById("tableContent").innerHTML = examRuleSubjectsTable.map((item, index) =>
        `<tr>
            <td class="col-sm-2">${item.SubjectName}</td>
            <td class="col-sm-2">${item.SubtopicName}</td>
            <td class="col-sm-2">${item.ExamType}</td>
            <td class="col-sm-2">${item.QuestionType}</td>
            <td class="col-sm-2">${item.QuestionDifficulty}</td>
            <td class="col-sm-2">${item.QuestionCount}</td>
            <td class="col-sm-2"><button class="btn btn-danger btn-sm" type="button" onclick="deleteRule(${index})"> X </button></td>
        </tr>`).join('');
}

async function clearExamRuleInputs() {
   
    document.getElementById("QuestionType").value = "";
    document.getElementById("QuestionDifficultyId").value = "";
    document.getElementById("QuestionCount").value = "";
}

