//Trainer TechnicalUnit Onchange Event
function getProducts() {
    const technicalUnitId = $("#TechnicalUnitId").val();
    $.ajax({
        type: "get",
        url: '../CallProductListTrainerUpdate',
        data: { technicalUnitId },
        success: function (response) {
            $(".trainer-product-component-container").empty();
            $(".trainer-product-component-container").append(response);
            showCheckboxes();
        }
    });
}
function showCheckboxes() {
    var checkboxes =
        document.getElementById("checkBoxes");
    checkboxes.style.display = "block";
}

window.addEventListener("load", showCheckboxes);