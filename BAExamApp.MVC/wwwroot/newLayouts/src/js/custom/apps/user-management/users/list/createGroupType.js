$('#updateSubjectModal').on('show.bs.modal', function (event) {
    let button = $(event.relatedTarget);
    var itemId = button.data('item-id');
    var itemName = button.data('item-name');
    var itemInformation = button.data('item-information');

    let modal = $(this);
    modal.find('input[name="Id"]').val(itemId);
    modal.find('input[name="Name"]').val(itemName);
    modal.find('textarea[name="Information"]').val(itemInformation);
});

// Uyarı
function showModalCloseConfirmation(modalSelector) {
    Swal.fire({
        title: localizedTexts.unsavedChangesTitle,
        text: localizedTexts.unsavedChangesText,
        icon: 'warning',
        showCancelButton: true,
        cancelButtonColor: '#3085d6',
        confirmButtonColor: '#d33',
        confirmButtonText: localizedTexts.confirmButtonText,
        cancelButtonText: localizedTexts.cancelButtonText
    }).then((result) => {
        if (result.isConfirmed) {
            // Geçici olarak event handler'ı kaldır
            $(modalSelector).off('hide.bs.modal');
            // Modalı kapat
            $(modalSelector).modal('hide');
            // Modal tamamen kapandıktan sonra event handler'ı tekrar ekle
            $(modalSelector).on('hidden.bs.modal', function () {
                addModalCloseConfirmation(modalSelector);
                $(this).find('form').trigger('reset');
            });
        }
    });
}

function addModalCloseConfirmation(modalSelector) {
    $(modalSelector).on('hide.bs.modal', function (e) {
        e.preventDefault();
        showModalCloseConfirmation(modalSelector);
    });
}

// İlk yüklemede event handler'ı ekle
addModalCloseConfirmation('#updateSubjectModal');
addModalCloseConfirmation('#createSubjectModal');

$('.groupTypeModal').on('hidden.bs.modal', function () {
    $(this).find('form').trigger('reset');
});