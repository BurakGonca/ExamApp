let questionAnswerChoices = document.getElementsByClassName("choices");
let studentAnswers = [];
let studentQuestonId = document.getElementById("StudentQuestionId").value;
let examTypeJSON = JSON.parse(document.getElementById("examType").value);
let timeGiven = document.getElementById("TimeGiven").value.split(':').reduce(function (seconds, v) {
    return + v + seconds * 60;
}, 0);

let timeStarted = new Date(document.getElementById("TimeStarted").value);

let questionTimer; // var kullanarak global olarak tanımla

document.getElementById("next-question-button").addEventListener("click", sendAnswer);

[...questionAnswerChoices].forEach(function (item) {
    studentAnswers.push({ IsSelected: false, StudentQuestionId: studentQuestonId, QuestionAnswerId: item.value });
});

document.getElementById("studentAnswers").value = JSON.stringify(studentAnswers);

function updateStudentAnswers() {
    studentAnswers.forEach((item, index) => {
        item.IsSelected = document.getElementById(`answer-${index + 1}`).checked;
    });
    document.getElementById("studentAnswers").value = JSON.stringify(studentAnswers);
}

document.getElementById("transitionCounterMain").style.display = "none";

if (examTypeJSON === 1) {
    questionTimer = setInterval(function () {

        let questionCounterText = "Soru İçin Kalan Süre: ";
        let counter = timeGiven - Math.floor((Date.now() - timeStarted) / 1000);

        if (counter > 5) {
            let minutes = Math.floor(counter / 60);
            let seconds = counter % 60;
            document.getElementById("questionCounter").innerHTML = formatTime(minutes) + ":" + formatTime(seconds);
            document.getElementById("questionCounterText").innerHTML = questionCounterText;
        } else if (counter > 0) {
            document.getElementById("questionCounter").style.color = "#e6353b";
            document.getElementById("questionCounter").innerHTML = formatTime(counter);
            document.getElementById("questionCounterText").innerHTML = questionCounterText;
        } else {
            document.getElementById("questionCounter").innerHTML = "0:00";
            document.getElementById("questionCounterText").innerHTML = questionCounterText;
            sendAnswer();
            document.getElementById("questionCounterMain").style.display = "none";
            document.getElementById("transitionCounterMain").style.display = "block";
            transitionCountdown();
        }
    }, 1000);
}

function formatTime(time) {
    return time < 10 ? "0" + time : time.toString();
}

function sendAnswer() {
    if (examTypeJSON === 1) {
        clearInterval(questionTimer);
    }
    [...questionAnswerChoices].forEach(function (item) {
        item.setAttribute("disabled", "true")
    });
    document.getElementById("frmQuestion").submit();
}

function transitionCountdown() {
    let transitionCounterTimeAmount = 5;
    let transitionCounterText = "";
    let transitionCounterElement = document.getElementById("transitionCounter");

    if (!transitionCounterElement) {
        console.error("transitionCounter element bulunamadı!");
        return;
    }

    if (questionCount !== currentQuestionOrder) {
        transitionCounterText = " Saniye Sonra Diğer Soruya Geçilecektir...";
        transitionCounterElement.style.background = "linear-gradient(to bottom, #ffffff, #e6e6e6)";
        transitionCounterElement.style.padding = "5px";
        transitionCounterElement.style.borderRadius = "10px";
        transitionCounterElement.style.textAlign = "center";
    } else {
        transitionCounterText = " Saniye Sonra Sınavınız Sona Erecektir...";
        transitionCounterElement.style.background = "linear-gradient(to bottom, #ffffff, #e6e6e6)";
        transitionCounterElement.style.padding = "5px";
        transitionCounterElement.style.borderRadius = "10px";
        transitionCounterElement.style.textAlign = "center";
    }

    let transitionTimer = setInterval(function () {
        if (transitionCounterTimeAmount > 0) {
            // Henüz 0'a ulaşmadıysa, kalan süreyi göster.
            transitionCounterElement.style.color = "#e6353b";
            transitionCounterElement.innerHTML = transitionCounterTimeAmount + " " +  transitionCounterText;
            transitionCounterTimeAmount--;
        } else {
            // Sayaç 0'a ulaştıysa, işlem durdurulmalı ve sonraki soruya geçilmeli.
            clearInterval(transitionTimer);
            document.getElementById("next-question-button").click();
        }
    }, 1000);
}
