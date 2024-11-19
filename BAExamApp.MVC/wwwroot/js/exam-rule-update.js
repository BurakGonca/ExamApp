//SelectLists
let subjects = [];
let subtopics = [];
let examRuleSubjects = [];
let examRuleSubjectsTable = [];
let questionTypes = [];
let questionDifficulties = [];
let examTypes = [];

onPageLoad();

async function onPageLoad() {
    let examRuleSubjectsJSON = JSON.parse(document.getElementById("examRuleSubjects").value);
    await onProductChange();
    for (examRuleSubject of examRuleSubjectsJSON) {
        await loadExamTypes();
        document.getElementById("ExamType").value = examRuleSubject.ExamType;
        await onExamTypeChange();

        var subtopics = await getSubtopics(examRuleSubject.SubjectId)
        var foundSubtopic = subtopics.find(subtopic => subtopic.value === examRuleSubject.SubtopicId);
        var questionDiff = await getQuestionDifficulties(examRuleSubject.ExamType)
        var foundQuestionDiff = questionDiff.find(questionDiff => questionDiff.value === examRuleSubject.QuestionDifficultyId)
        var examTypes = await getExamTypes();
        var foundExamType = examTypes.find(examType => Number(examType.value) === examRuleSubject.ExamType)
        var questionTypes = await getQuestionTypes(examRuleSubject.ExamType)
        var foundQuestionType = questionTypes.find(questionType => Number(questionType.value) === examRuleSubject.QuestionType)

        let examRuleSubjectVM = {
            SubjectId: examRuleSubject.SubjectId,
            SubtopicId: examRuleSubject.SubtopicId,
            ExamType: examRuleSubject.ExamType,
            QuestionType: examRuleSubject.QuestionType,
            QuestionDifficultyId: examRuleSubject.QuestionDifficultyId,
            QuestionCount: examRuleSubject.QuestionCount,
        }
        examRuleSubjects.push(examRuleSubjectVM);

        examRuleSubjectsTable.push({
            SubjectName: await getSubjectNameBySubjectId(examRuleSubject.SubjectId),
            SubtopicName: foundSubtopic.text,
            ExamType: foundExamType.text,
            QuestionType: foundQuestionType.text,
            QuestionDifficulty: foundQuestionDiff.text,
            QuestionCount: examRuleSubject.QuestionCount
        });
    }
    updateTable();
}

//Product type change event.
async function onProductChange() {
    subjects = subjects ? await getSubjects($("#ProductId").val()) : subjects;
    populateSelectList("SubjectId", subjects);

    refreshModal();

    examRuleSubjects = [];
    examRuleSubjectsTable = [];

    updateTable();
    document.getElementById("examRuleSubjects").value = JSON.stringify(examRuleSubjects);
}

//Product type change event.
async function onSubjectChange() {
    subtopics = subtopics ? await getSubtopics($("#SubjectId").val()) : subtopics;
    populateSelectList("SubtopicId", subtopics);
}

//Exam type change event.
async function onExamTypeChange() {
    questionTypes = questionTypes ? await getQuestionTypes($("#ExamType").val()) : questionTypes;
    questionDifficulties = questionDifficulties ? await getQuestionDifficulties($("#ExamType").val()) : questionDifficulties;

    populateSelectList("QuestionType", questionTypes);
    populateSelectList("QuestionDifficultyId", questionDifficulties);

    questionTypes = [];
    questionDifficulties = [];
};

//Add new rule
async function addNewRule() {
    if (document.getElementById("Name").value && document.getElementById("ProductId").value && document.getElementById("SubjectId").value && document.getElementById("SubtopicId").value && document.getElementById("ExamType").value && document.getElementById("QuestionType").value && document.getElementById("QuestionDifficultyId").value && document.getElementById("QuestionCount").value && document.getElementById("Description").value) {

        document.getElementById("inputValidation").setAttribute("hidden", true);
        document.getElementById("examTypeValidation").setAttribute("hidden", true);


        let examRuleSubjectVM = {
            SubjectId: document.getElementById("SubjectId").value,
            SubtopicId: document.getElementById("SubtopicId").value,
            ExamType: parseInt(document.getElementById("ExamType").value),
            QuestionType: parseInt(document.getElementById("QuestionType").value),
            QuestionDifficultyId: document.getElementById("QuestionDifficultyId").value,
            QuestionCount: parseInt(document.getElementById("QuestionCount").value),
        }

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
                    QuestionCount: parseInt(document.getElementById("QuestionCount").value)
                })
            }
            else {
                document.getElementById("sameRuleValidation").removeAttribute("hidden");
            }
            refreshModal();
        }
        else {
            document.getElementById("examTypeValidation").removeAttribute("hidden");
        }

    }
    else {
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
    let selectList = `<option value="" disabled="" selected="">--- Seçiniz ---</option>`.concat(selectListOptions);
    document.getElementById(selectListId).innerHTML = selectList;
}

async function refreshModal() {
    updateTable();
    clearExamRuleInputs()
    document.getElementById("examRuleSubjects").value = JSON.stringify(examRuleSubjects);
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
            <td class="col-sm-2"><button class="btn btn-danger btn-sm" type="button" onclick="deleteRule(${index})">X</button></td>
        </tr>`).join('');
}

async function clearExamRuleInputs() {
    document.getElementById("SubjectId").value = "";
    document.getElementById("SubtopicId").value = "";    
    document.getElementById("QuestionType").value = "";
    document.getElementById("QuestionDifficultyId").value = "";
    document.getElementById("QuestionCount").value = "";
}

//Ajax functions for getting selectList
async function getExamRuleSubjects(examRuleId) {
    return $.ajax({
        url: '/Admin/ExamRule/GetExamRuleSubjectsByExamRuleId',
        data: { examRuleId: examRuleId },
    });
}

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

async function getQuestionDifficulties(selectedExamTypeId) {

    return $.ajax({
        url: '/Admin/ExamRule/GetQuestionDifficulties',
        data: { examTypeId: selectedExamTypeId }
    });
}

async function loadExamTypes() {
    examTypes = examTypes ? await getExamTypes() : examTypes;

    populateSelectList("ExamType", examTypes);
}

function getExamTypes() {
    return $.ajax({
        url: '/Admin/ExamRule/GetExamTypes'
    });
}

async function getSubjectNameBySubjectId(subjectId) {

    return $.ajax({
        url: '/Admin/ExamRule/GetSubjectNameBySubjectId',
        data: { subjectId: subjectId }
    });
}
