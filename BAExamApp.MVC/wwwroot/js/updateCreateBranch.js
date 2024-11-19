$('#updateBranchModal').on('show.bs.modal', function (event) {
    let button = $(event.relatedTarget);
    let branchId = button.data('branch-id');
    let branchName = button.data('branch-name');

    let modal = $(this);
    modal.find('input[name="Name"]').val(branchName);
    modal.find('input[name="Id"]').val(branchId);
});

// Alert
function showModalCloseConfirmation(modalId) {
    Swal.fire({
        title: localize.unsavedChangesTitle,
        text: localize.unsavedChangesText,
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: localize.confirmButtonText,
        cancelButtonText: localize.cancelButtonText
    }).then((result) => {
        if (result.isConfirmed) {
            // Event handler'ı geçici olarak kaldır
            $('#' + modalId).off('hide.bs.modal');
            // Modalı kapat
            $('#' + modalId).modal('hide');
            // Modal tamamen kapandıktan sonra event handler'ı tekrar ekle
            $('#' + modalId).on('hidden.bs.modal', function () {
                addModalCloseConfirmation(modalId);
                $(this).find('form').trigger('reset');
            });
        }
    });
}

function addModalCloseConfirmation(modalId) {
    $('#' + modalId).on('hide.bs.modal', function (e) {
        e.preventDefault();
        showModalCloseConfirmation(modalId);
    });
}

// İlk yüklemede event handler'ı ekle
$(document).ready(function () {
    addModalCloseConfirmation('updateBranchModal');
    addModalCloseConfirmation('createBranchModal');
});


$('#updateBranchModal, #createBranchModal').on('hidden.bs.modal', function () {
    $(this).find('form').trigger('reset');
});