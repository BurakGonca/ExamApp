async function StartExam(examId) {
    const confirmation = await Swal.fire({
        text: localizedTexts.startExamConfirmation,
        icon: 'question',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: localizedTexts.yesStart,
        cancelButtonText: localizedTexts.noCancel
    });

    if (confirmation.value) {
        var loader = StartExamLoader();

        $.ajax({
            url: '/CandidateAdmin/CandidateExam/StartExamAndNotifyCandidates',
            data: { id: examId },
            success: function (data) {
                if (data.success) {
                    location.reload();
                } else {
                    Swal.fire({
                        icon: 'error',
                        title: 'Oops...',
                        text: data.message
                    });
                }
            },
            error: function (xhr, status, error) {
                Swal.fire({
                    icon: 'error',
                    title: 'Oops...',
                    text: 'Bir hata oluştu, lütfen tekrar deneyin.'
                });
            },
            complete: function () {
                for (let i = 0; i < loader.length; i++) {
                    loader[i].style.display = 'none';
                }
                loader.textContent = '';
            }
        });
    }
};



function StartExamLoader() {
    const loadingStartExam = document.getElementsByClassName('exam-loading');
    const loadingSpanExam = document.getElementById('exam-loading-span');
    if (!loadingSpanExam) {
        console.error("exam-loading-span elementi bulunamadı!");
        return;
    }
    loadingSpanExam.textContent = 'SINAV BAŞLATILIYOR...';
    loadingSpanExam.style.left = '-22%';
    loadingSpanExam.style.letterSpacing = '5px';
    loadingSpanExam.style.color = '#dfeff7';
    loadingSpanExam.style.width = '500px';

    for (let i = 0; i < loadingStartExam.length; i++) {
        loadingStartExam[i].style.display = 'block';
    }

    return loadingStartExam;
}
