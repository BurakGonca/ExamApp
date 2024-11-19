function showTable(tableId) {
    var table = document.getElementById(tableId);
    var otherTables = document.querySelectorAll('div[id$="-exams"]');
    for (var i = 0; i < otherTables.length; i++) {
        if (otherTables[i].id !== tableId) {
            otherTables[i].style.display = "none";
        }
    }
    if (table.style.display !== "block") {
        table.style.display = "block";
    }
}

const alertTrigger = document.getElementById('liveAlertBtn');
let isAlertShown = false;

if (alertTrigger) {
    alertTrigger.addEventListener('click', () => {
        if (!isAlertShown) {
            appendAlert('Sınav henüz başlamamıştır.', 'warning');
            isAlertShown = true;
        }
    });
}

function appendAlert(message, type) {
    const alertPlaceholder = document.getElementById('liveAlertPlaceholder');
    alertPlaceholder.innerHTML = '';

    const wrapper = document.createElement('div');
    wrapper.innerHTML = `
        <div class="alert alert-${type} alert-dismissible" role="alert">
            <div>${message}</div>
            <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>
        </div>
    `;

    alertPlaceholder.append(wrapper);
}