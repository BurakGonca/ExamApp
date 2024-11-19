
document.querySelector("#fileInput").addEventListener("change", function (event) {
    let file = event.target.files[0];
    let allowedTypes = ["image/jpeg", "image/png", "image/jpg"];
    let errorElement = document.querySelector("[data-valmsg-for='NewImage.ContentType']");

    if (allowedTypes.includes(file.type)) {
        let reader = new FileReader();
        reader.onload = function () {
            document.querySelector("#previewImage").src = reader.result;
            document.querySelector("#previewImage").style.display = "block";
            document.querySelector("#defaultImage").style.visibility = "hidden";
            errorElement.innerHTML = "";
        };
        reader.readAsDataURL(file);
    } else {
        errorElement.innerHTML = validationMessage;
        document.querySelector("#previewImage").style.display = "none";
        document.querySelector("#defaultImage").style.visibility = "visible";
        document.querySelector("#fileInput").value = "";
    }
});

$("#addPreview").click(function () {
    $("#fileInput").click();
});

$("#removePreview").click(function () {
    $("#fileInput").val("");
    document.querySelector("#previewImage").src = "";
    document.querySelector("#previewImage").style.display = "none";
    document.querySelector("#defaultImage").style.visibility = "visible";
    var validationSpan = document.querySelector("span[data-valmsg-for='NewImage.ContentType']");
    validationSpan.textContent = "";
});

$("#removePicture").click(function () {
    $("#fileInput").val("");
    document.querySelector("#previewImage").src = "";
    document.querySelector("#previewImage").style.display = "none";
    document.querySelector("#defaultImage").style.visibility = "visible";
    document.querySelector("#defaultImage").src = "/images/blank-profile-picture.png";
    var validationSpan = document.querySelector("span[data-valmsg-for='NewImage.ContentType']");
    validationSpan.textContent = "";
});

function removeImage() {
    document.getElementById("pictureText").value = "";
}