var show = true;

function showProducts() {
    var products =
        document.getElementById("products");


    if (show) {
        products.style.display = "block";
        show = false;
    } else {
        products.style.display = "none";
        show = true;
    }
}