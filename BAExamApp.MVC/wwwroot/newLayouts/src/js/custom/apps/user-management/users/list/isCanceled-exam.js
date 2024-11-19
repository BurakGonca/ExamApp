const cancelExamButton = document.getElementById('cancel-exam');

function cancelExamById(id) {
    console.log(id)
    Swal.fire({
        title: `${localizedTexts.confirmCancelTitle}`,
        text: localizedTexts.confirmCancelText,
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#d33',
        cancelButtonColor: '#3085d6',
        confirmButtonText: localizedTexts.confirmCancelButtonText,
        cancelButtonText: localizedTexts.cancelCancelButtonText
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                type: "POST",
                data: { examId: id },
                url: "/Admin/Exam/IsCanceled",
                success: function (result) {
                    if (result.isSuccess) {
                        setTimeout(() => location.href = "/Admin/Exam", 2000)
                    }

                }
            });
        }
    });
}