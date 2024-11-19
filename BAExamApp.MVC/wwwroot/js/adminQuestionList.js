
function submitForm(questionId) {
    // Ajax isteği ile Approve aksiyonunu çağırabilirsiniz
    // Örnek jQuery kullanımı:
    $.post('/Admin/Question/Approve', { id: questionId })
        .done(function (data) {
            // Başarı durumu
            console.log(data);
            // Gerekirse sayfayı güncelleme veya başka işlemler yapma
            location.reload();
        })
        .fail(function (error) {
            // Hata durumu
            console.error(error);
        });
}