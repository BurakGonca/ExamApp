const deleteExamButton = document.getElementById('delete-exam');

function deleteExamById(id) {
    console.log(id)
    Swal.fire({
            title: `${localizedTexts.confirmTitle}`,
            text: localizedTexts.confirmText,
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#d33',
            cancelButtonColor: '#3085d6',
            confirmButtonText: localizedTexts.confirmDeleteButtonText,
            cancelButtonText: localizedTexts.cancelDeleteButtonText
        }).then((result) => {
            if (result.isConfirmed) {
                $.ajax({
                    type: "POST",
                    data: { examId: id },
                    url: "/Admin/Exam/DeleteExam",
                    success: function (result) {
                        if (result.isSuccess) {
                            setTimeout(() => location.href = "/Admin/Exam", 2000)
                        }

                    }
                });
            }
        });
}