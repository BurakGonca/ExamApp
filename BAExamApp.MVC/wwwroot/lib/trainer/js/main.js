let aside = document.querySelector("aside");
let li = aside.getElementsByClassName("nav-item");


for (let i of li) {
    i.onclick = activeLi;
}


function activeLi() {
    const list = Array.from(li);
    list.forEach((e) => e.classList.remove("active"));
    this.classList.add("active");
}


aside.addEventListener("mouseenter", () => {
    aside.classList.add("expand");
});

aside.addEventListener("mouseleave", () => {
    aside.classList.remove("expand");
});


const userIcon = document.querySelector('.user-icon');
const dropdownMenu = document.querySelector('.dropdown-menu');

let isDropdownVisible = false;
let isMouseOverDropdown = false;

userIcon.addEventListener('mouseenter', () => {
    isDropdownVisible = true;
    dropdownMenu.style.display = 'block';
});

dropdownMenu.addEventListener('mouseleave', () => {
    isDropdownVisible = false;
    dropdownMenu.style.display = 'none';
});

dropdownMenu.addEventListener('mouseenter', () => {
    isMouseOverDropdown = true;
});

dropdownMenu.addEventListener('mouseleave', () => {
    isMouseOverDropdown = false;
});

document.addEventListener('click', (event) => {
    if (!isMouseOverDropdown && !event.target.closest('.user-icon-container')) {
        isDropdownVisible = false;
        dropdownMenu.style.display = 'none';
    }
});


function toggleDropdown(menuId) {
    var dropdowns = document.getElementsByClassName("options-dropdown-menu");

    for (var i = 0; i < dropdowns.length; i++) {
        var openDropdown = dropdowns[i];
        if (openDropdown.id === menuId) {
            openDropdown.classList.toggle("show");
        } else {
            openDropdown.classList.remove("show");
        }
    }
}

window.onclick = function (event) {
    if (!event.target.matches('.btn-details')) {
        var dropdowns = document.getElementsByClassName("options-dropdown-menu");
        for (var i = 0; i < dropdowns.length; i++) {
            var openDropdown = dropdowns[i];
            openDropdown.classList.remove("show");
        }

    }
}

// Modalı açan fonksiyon
function openQuestionModal(questionId) {
    var modal = document.getElementById("questionModal");
    modal.style.display = "block";
    getQuestionDetails(questionId);

}

function getQuestionDetails(questionId) {

    var myUrl = "/Trainer/Question/Details/" + questionId;

    $.ajax({
        url: myUrl,
        method: "GET",
        success: function (response) {
            $('#question-details').html(response)
        },
        error: function () {
            modal.hide()
            Swal.fire(
                'Hata !',
                'İstediğiniz soruya ait detaylar alınamadı. Lütfen sistem yöneticinize başvurun. (Error 404)',
                'error'
            )
        }
    })
}

// Modalı kapatan fonksiyon
function closeQuestionModal() {
    var modal = document.getElementById("questionModal");
    modal.style.display = "none";
}

// Kapatma düğmesine tıklandığında modalı kapat
var closeBtn = document.querySelector(".close");
closeBtn.addEventListener("click", closeQuestionModal);




function deleteQuestion(questionId) {
    console.log(questionId);
    var myUrl = "/Trainer/Question/Delete/" + questionId;
    console.log(myUrl)
    Swal.fire({
        title: 'Emin misiniz?',
        text: "Bu işlem geri alınamaz!",
        icon: 'warning',
        iconColor: '#f33c44',
        showCancelButton: true,
        confirmButtonColor: '#04b0f4',
        cancelButtonColor: '#f33c44',
        confirmButtonText: 'Evet, sil!',
        cancelButtonText: 'İptal',
    }).then((e) => {
        if (e.isConfirmed) {
            $.ajax({
                type: "GET",
                url: myUrl,
                success: function (data) {
                    location.reload();
                }
            });
        }
    });
}

function deleteExam(examId) {
    var myUrl = "/Trainer/Exam/Delete/" + examId;
    Swal.fire({
        title: 'Emin misiniz?',
        text: "Bu işlem geri alınamaz!",
        icon: 'warning',
        iconColor: '#f33c44',
        showCancelButton: true,
        confirmButtonColor: '#04b0f4',
        cancelButtonColor: '#f33c44',
        confirmButtonText: 'Evet, sil!',
        cancelButtonText: 'İptal',
    }).then((e) => {
        if (e.isConfirmed) {
            $.ajax({
                type: "GET",
                url: myUrl,
                success: function (data) {
                    location.reload();
                }
            });
        }
    });
}

function previewImage(event) {
    var input = event.target;
    if (input.files && input.files[0]) {
        var reader = new FileReader();

        reader.onload = function (e) {
            document.getElementById("profile-img").src = e.target.result;
        }

        reader.readAsDataURL(input.files[0]);
    }
}
// Soru detayları alanında sorunun kopyalanabilmesi için oluşturuldu. İlgili methodda id'yi temp dataya kaydediyor.'
//function getQuestion(questionId) {
//    console.log("success copy: " + questionId)
//    var myUrl = "/Trainer/Question/GetQuestionId/" + questionId;
//    $.ajax({
//        type: "GET",
//        url: myUrl,
//        success: function (data) {
//        }
//    });
//}

function showButton() {
    var fileInput = document.getElementById('file');
    var showData = document.getElementById('showData');

    var selectedFile = fileInput.files[0];

    if (selectedFile) {
        var fileName = selectedFile.name;
        var fileExtension = fileName.split('.').pop().toLowerCase();

        if (fileExtension === 'xlsx') {
            showData.style.display = 'block';
        } else {
            alert('Lütfen sadece .xlsx uzantılı dosya seçiniz.');
            showData.style.display = 'none';
        }
    } else {
        showData.style.display = 'none';
    }
}

function checkAll() {
    var allCheck = document.getElementById("AllCheck");
    var checkboxes = document.querySelectorAll("input[type='checkbox']");

    for (var i = 0; i < checkboxes.length; i++) {
        checkboxes[i].checked = allCheck.checked;
    }
}

function ExcelDataPost() {
    var form = document.getElementById('excelTable');
    form.addEventListener('submit', function (e) {
        e.preventDefault();
        var checkboxes = document.querySelectorAll('input[type="checkbox"]:checked');

        var selectedData = [];

        checkboxes.forEach(function (checkbox) {
            var rowData = checkbox.closest('tr').querySelectorAll('td');

            if (rowData.length != 0) {
                var data = {
                    QuestionType: rowData[1].textContent,
                    Content: rowData[2].textContent,
                    Target: rowData[3].textContent,
                    Gains: rowData[4].textContent,
                    SubtopicId: rowData[5].textContent,
                    ProductId: rowData[6].textContent,
                    SubjectId: rowData[7].textContent,
                    QuestionDifficultyId: rowData[8].textContent,
                    TimeGiven: rowData[9].textContent,
                    QuestionAnswers: [],
                };

                for (var column = 10; column < rowData.length; column += 2) {
                    var answerData = {
                        IsRightAnswer: rowData[column].textContent,
                        Answer: rowData[column + 1].textContent
                    };
                    if (data.QuestionAnswers) {
                        data.QuestionAnswers.push(answerData);
                    } else {
                        data.QuestionAnswers = [answerData];
                    }
                }

                selectedData.push(data);
            }
        });

        var jsonData = JSON.stringify(selectedData);

        var xhr = new XMLHttpRequest();
        xhr.open('POST', 'SaveExcelList', true);
        xhr.setRequestHeader('Content-Type', 'application/json');

        xhr.onreadystatechange = function () {
            if (xhr.readyState === XMLHttpRequest.DONE) {
                if (xhr.status === 200) {
                    window.location.href = '/Trainer/Question/QuestionList?state=Awaited';
                } else {
                    window.location.href = '/Trainer/Question/QuestionTable';
                }
            }
        };

        xhr.send(jsonData);
    });
}


