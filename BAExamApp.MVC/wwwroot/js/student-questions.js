window.addEventListener('DOMContentLoaded', function () {
    var questions = document.getElementsByClassName('question');
    if (questions.length > 0) {
        questions[0].style.display = 'block';
        startTimer(questions[0].id); // Task 1330 
    }
});

//function previousQuestion() {
//    var questions = document.getElementsByClassName('question');
//    var currentQuestion = getCurrentQuestionIndex();

//    if (currentQuestion > 0) {

//        questions[currentQuestion].style.display = 'none';
//        stopTimer(questions[currentQuestion].id); // Task 1330
//        questions[currentQuestion - 1].style.display = 'block';
//        startTimer(questions[currentQuestion - 1].id); // Task 1330
//    }
//}

//function nextQuestion(examStudentId) {
//    var questions = document.getElementsByClassName('question');
//    var currentQuestion = getCurrentQuestionIndex();
//    if (currentQuestion < questions.length - 1) {
//        questions[currentQuestion].style.display = 'none';
//        stopTimer(questions[currentQuestion].id); // Task 1330
//        questions[currentQuestion + 1].style.display = 'block';
//        startTimer(questions[currentQuestion + 1].id); // Task 1330
//    }
//}

function nextQuestion(examStudentId) {
    var questions = document.getElementsByClassName('question');
    var currentQuestionIndex = getCurrentQuestionIndex();
    Swal.fire({
        title: 'Sonraki soruya geçmek istediğinize emin misiniz?',
        text: "Bu soruyu geçtikten sonra bir daha geri dönemezsiniz! Soruyu boş bırakmadığınıza ve doğru düşündüğünüz cevabı verdiğinize lütfen emin olun!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Evet, Sonraki soru!',
        cancelButtonText: 'Hayır, Henüz soruyu bitirmedim!'
    }).then((result) => {
        if (result.isConfirmed) {
            if (currentQuestionIndex >= 0 && currentQuestionIndex < questions.length - 1) {
                var currentQuestion = questions[currentQuestionIndex];
                var nextQuestion = questions[currentQuestionIndex + 1];

                // Şu anki sorunun cevaplarını gönderir
                sendAnswer(examStudentId, currentQuestion.id);

                // Şu anki soruyu gizle, sonraki soruyu göster
                currentQuestion.style.display = 'none';
                stopTimer(currentQuestion.id);
                nextQuestion.style.display = 'block';
                startTimer(nextQuestion.id);

                if (questions.length - 1 == currentQuestionIndex + 1) {
                    let nextButton = document.getElementById('nextButton');
                    let finihExamButton = document.getElementById('finishExamButton');
                    nextButton.style.display = 'none';
                    finihExamButton.style.display = 'block';
                }
            }
        }
    });


}

function nextQuestionForTime(examStudentId) {
    var questions = document.getElementsByClassName('question');
    var currentQuestionIndex = getCurrentQuestionIndex();

    if (currentQuestionIndex >= 0 && currentQuestionIndex < questions.length - 1) {
        var currentQuestion = questions[currentQuestionIndex];
        var nextQuestion = questions[currentQuestionIndex + 1];

        // Şu anki sorunun cevaplarını gönderir
        sendAnswer(examStudentId, currentQuestion.id);

        // Şu anki soruyu gizle, sonraki soruyu göster
        currentQuestion.style.display = 'none';
        stopTimer(currentQuestion.id);
        nextQuestion.style.display = 'block';
        startTimer(nextQuestion.id);

        if (questions.length - 1 == currentQuestionIndex + 1) {
            let nextButton = document.getElementById('nextButton');
            let finihExamButton = document.getElementById('finishExamButton');
            nextButton.style.display = 'none';
            finihExamButton.style.display = 'block';
        }
    }
}

function getCurrentQuestionIndex() {
    var questions = document.getElementsByClassName('question');

    for (var i = 0; i < questions.length; i++) {
        if (questions[i].style.display === 'block') {
            return i;
        }
    }

    return -1;
}

function goToQuestion() {
    var questionNumber = parseInt(document.getElementById('soruNumarasi').value);
    var questions = document.getElementsByClassName('question');

    for (var i = 0; i < questions.length; i++) {
        questions[i].style.display = 'none';
        stopTimer(questions[i].id); // Task 1330
    }

    if (questionNumber >= 1 && questionNumber <= questions.length) {
        questions[questionNumber - 1].style.display = 'block';
        startTimer(questions[questionNumber - 1].id); // Task 1330
    } else {
        alert("Geçersiz soru numarası!");
    }
}

function confirmFinishExam(examStudentId) {
    Swal.fire({
        title: 'Sınavı bitirmek istediğinize emin misiniz?',
        text: "Bitirdikten sonra bir daha bu sınava giremezsiniz",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Evet, bitir!',
        cancelButtonText: 'İptal'
    }).then((result) => {
        if (result.isConfirmed) {
            sendAnswers(examStudentId);
        }
    });
}

//Task 1431 ↓

function sendAnswer(examStudentId, questionId) {
    var formData = new FormData();
    var question = document.getElementById(questionId);
    var selectedAnswers = [];

    // Soru süresi
    var dataTime = parseInt(question.querySelector(".timer").getAttribute("data-time"));

    // Cevapları topla
    var radioButtons = question.querySelectorAll('input[type="radio"][name="cevap"]:checked');
    var checkboxes = question.querySelectorAll('input[type="checkbox"][name="cevap"]:checked');
    var textarea = question.querySelector('textarea');
    var fileInput = question.querySelector('input[type="file"]');

    // "Like" seçeneğini yakala
    var likeRadio = question.querySelector('input[type="checkbox"][name="like"]:checked');

    for (var j = 0; j < radioButtons.length; j++) {
        selectedAnswers.push(radioButtons[j].value);
    }

    for (var k = 0; k < checkboxes.length; k++) {
        selectedAnswers.push(checkboxes[k].value);
    }

    if (textarea) {
        var textareaValue = textarea.value.replace(/\r?\n/g, '');
        selectedAnswers.push(textareaValue);
    }

    var likeDislike = likeRadio ? true : null;

    formData.append('questionId', questionId);
    formData.append('TimeSpent', dataTime);
    formData.append('examStudentId', examStudentId);
    formData.append('LikeDislikeStatus', likeDislike);

    for (var l = 0; l < selectedAnswers.length; l++) {
        formData.append('answers', selectedAnswers[l]);
    }

    if (fileInput && fileInput.files.length > 0) {
        var file = fileInput.files[0];
        formData.append('fileAnswer', file, file.name);
    }

    $.ajax({
        url: "/Student/Exam/StudentQuestionAnswer",
        type: "POST",
        data: formData,
        processData: false,
        contentType: false,
        success: function () {
            console.log("Cevaplar gönderildi.");
            console.log(formData);
        },
        error: function (xhr, status, error) {
            console.error("Cevapları gönderme hatası: " + error);
        }
    });
}

//Task 1431 ↑

function sendAnswers(examStudentId) {
    var questions = document.getElementsByClassName('question');
    var formData = new FormData();

    for (var i = 0; i < questions.length; i++) {
        var question = questions[i];
        var questionId = question.id;
        var selectedAnswers = [];
        var dataTime = parseInt(question.querySelector(".timer").getAttribute("data-time")); // Task-1334

        var radioButtons = question.querySelectorAll('input[type="radio"][name="cevap"]:checked');
        var checkboxes = question.querySelectorAll('input[type="checkbox"][name="cevap"]:checked');
        var textarea = question.querySelector('textarea');
        var fileInput = question.querySelector('input[type="file"]');


        // Like radio buttonları yakalama.
        var likeRadio = question.querySelector('input[type="checkbox"][name="like"]:checked');

        for (var j = 0; j < radioButtons.length; j++) {
            selectedAnswers.push(radioButtons[j].value);
        }

        for (var k = 0; k < checkboxes.length; k++) {
            selectedAnswers.push(checkboxes[k].value);
        }

        if (textarea) {
            var textareaValue = textarea.value.replace(/\r?\n/g, '');
            selectedAnswers.push(textarea.value);
        }

        if (likeRadio == null)
            var likeDislike = null;
        else
            var likeDislike = true;

        formData.append('answers[' + i + '].questionId', questionId);
        formData.append('answers[' + i + '].TimeSpent', dataTime); // Task-1334
        formData.append('answers[' + i + '].examStudentId', examStudentId);
        formData.append('answers[' + i + '].LikeDislikeStatus', likeDislike);
        for (var l = 0; l < selectedAnswers.length; l++) {
            formData.append('answers[' + i + '].answers', selectedAnswers[l]);
        }
        if (fileInput && fileInput.files.length > 0) {
            var file = fileInput.files[0];
            formData.append('answers[' + i + '].fileAnswer', file, file.name);
        }
    }

    $.ajax({
        url: "/Student/Exam/StudentQuestionAnswers",
        type: "POST",
        data: formData,
        processData: false,
        contentType: false,
        success: function () {
            window.location.href = "/Student/Exam/GetCurrentExamsList"
        }
    });
}

// Task 1330 ↓ Kronometre yönetimi

var timers = {}; // Her soru için ayrı kronometre tutan değişken

// Kronometreyi başlatan fonksiyon
function startTimer(questionId,examStudentId) {

    var display = document.getElementById(questionId + "timer")
    var timer = parseInt(display.getAttribute("data-time"));
    
    var interval = setInterval(function () {
        var hours = Math.floor(timer / 3600);
        var minutes = Math.floor((timer % 3600) / 60);
        var seconds = timer % 60;

        var hoursStr = hours < 10 ? "0" + hours : hours.toString();
        var minutesStr = minutes < 10 ? "0" + minutes : minutes.toString();
        var secondsStr = seconds < 10 ? "0" + seconds : seconds.toString();

        display.textContent = hoursStr + ":" + minutesStr + ":" + secondsStr;

        if (--timer < 0) {
            timer = 0;
            clearInterval(interval);
            nextQuestionForTime(examStudentId);
        } else if (timer <= 5) {
            display.style.color = "red";
        }
    }, 1000);
    timers[questionId] = interval;
}

// Kronometreyi durduran fonksiyon
function stopTimer(questionId) {
    clearInterval(timers[questionId]);
}

function updateTimer(questionId) {

    // Soruyu içeren div'in Id'si ile timer sınıfına sahip span yakalanır.
    var timerElement = document.querySelector('.timer');

    // Yakalanan spanın data-time özniteliğinin değeri okunur.
    var time = parseFloat(timerElement.getAttribute("data-time"));

    // Yakalanın Sorunun ekranda gösterilip gösterilmediği kontrol edilir.
    if (document.getElementById(questionId).style.display === "block") {
        time -= 1;
    }
    // data-time özniteliğinden elde edilen veriye göre dakika hesaplanır.
    var minutes = Math.floor(time / 60);

    // Saniye hesaplanır
    var seconds = time % 60;

    // Zaman bilgisi span içine yazılarak öğrenciye gösterilir.
    timerElement.innerHTML = minutes + ":" + (seconds < 10 ? "0" : "") + seconds;

    // timer class'ına sahip span'ın data-time özniteliğinin değeri toplam süreyi saniye cinsinden tutacak biçimde güncellenir.
    timerElement.setAttribute("data-time", time);
}

// Task 1330 ↑

