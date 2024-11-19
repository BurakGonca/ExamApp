

let examRules = [];

async function getExamRules(selectedExamTypeId) {
    return $.ajax({
        url: '/Trainer/Exam/GetExamRulesByExamType',
        data: { examTypeId: selectedExamTypeId }
    });
}

async function onExamTypeChange() {
    examRules = examRules ? await getExamRules($("#ExamType").val()) : examRules;
    populateSelectList("ExamRuleId", examRules);

    examRules = [];
};

function resetSelections() {
    $("#ExamType").prop('selectedIndex', 0);
    $("#ExamRuleId").prop('selectedIndex', 0);
    $("#ExamCreationType").prop('selectedIndex', 0);
}

async function populateSelectList(selectListId, data) {
    let selectListOptions = data.map((item, index) => {
        let title = item.group && item.group.name ? item.group.name : "Açıklama Bulunmamaktadır";
        return `<option value="${item.value}" title="${title}">${item.text}</option>`;
    });
    let selectList = `<option value="" disabled="" selected="">--- Seçiniz ---</option>`.concat(selectListOptions);
    document.getElementById(selectListId).innerHTML = selectList;
}