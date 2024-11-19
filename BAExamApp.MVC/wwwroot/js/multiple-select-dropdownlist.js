function setInitialValues() {
    var selectedItems = $('.pillbox-dropdown select').val();
    var selectedItemsHtml = '';
    if (selectedItems && selectedItems.length > 0) {
        selectedItems.forEach(function (value) {
            var selectedOption = $('.pillbox-dropdown select option[value="' + value + '"]');
            var selectedItemHtml = '<span class="pillbox-selected-item">' + selectedOption.text() + '</span>';
            selectedItemsHtml += selectedItemHtml;
            selectedOption.attr('selected', true);
        });
    }
    $('.pillbox-selected-items').html(selectedItemsHtml);
}