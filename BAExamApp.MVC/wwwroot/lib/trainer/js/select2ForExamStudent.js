$(document).ready(function () {
    $('#studentSelect').select2();
});
$(document).ready(function () {
    $('.pillbox-select2').select2({
        placeholder: "@ViewLocalizer["Name_Of_Trainers"]",
        allowClear: true,
        closeOnSelect: false,
        tags: true,
        templateResult: function (state) {
            if (!state.id) { return state.text; }
            return $("<div><input type='checkbox' id='checkbox-" + state.id + "' class='form-check-input' " + (state.selected ? "checked" : "") + "> <label for='checkbox-" + state.id + "'>" + state.text + "</label></div>")
                .click(function (e) {
                    if ($(e.target).is("input")) {
                        $('.pillbox-select2').trigger('change');
                    }
                });
        },
        templateSelection: function (state) {
            if (!state.id) { return state.text; }
            return $("<div style='padding-left: 22px'>" + state.text + "</div>");

        },
    });
});