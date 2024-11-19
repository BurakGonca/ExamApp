document.addEventListener('DOMContentLoaded', function () {
    var calendarEl = document.getElementById('kt_calendar_app');
    var calendar = new FullCalendar.Calendar(calendarEl, {
        headerToolbar: {
            left: "title",
            center: "",
            right: "dayGridMonth,timeGridDay",
        },
        footerToolbar: {
            left: "",
            center: "",
            right: "prev,next",
        },
        initialView: 'dayGridMonth',
        locale: selectedLanguage,  //layout'dan gelen dil verisine g�re takvimin dilini de�i�tiriyoruz. selectedLanguage de�i�keni admin layout'da tan�mlanm��t�r!!!
        dayMaxEvents: 0,
        moreLinkClick : 'popover',
        eventMouseEnter: function (event) {
            eventDataTemplate.startDate = event.event.start;
            eventDataTemplate.endDate = event.event.end;
            eventDataTemplate.eventName = event.event.title;
            showPopover(event.el);
        },
        eventMouseLeave: function (info) {
            hidePopover();
        },
        eventClick: function (event) {
            window.location.href = '/admin/exam/details?id=' + event.event.id;
            hidePopover();
            showModal();
        },
        datesSet: function (info) {
            var cdate = calendar.getDate();
            var month = cdate.getMonth() + 1;
            var year = cdate.getFullYear();
            var url = '/Admin/Home/GetEvents?year=' + year + '&month=' + month;

            document.getElementById('loading-overlay').classList.remove('d-none');
            calendarEl.style.filter = 'blur(3px)';
            fetch(url)
                .then(response => {
                    if (!response.ok) {
                        throw new Error('Network response was not ok');
                    }
                    return response.json();
                })
                .then(events => {
                    calendar.removeAllEvents();
                    calendar.addEventSource(events);
                })
                .catch(error => {
                    console.error('Error fetching events:', error);
                }).finally(() => {
                    document.getElementById('loading-overlay').classList.add('d-none');
                    calendarEl.style.filter = 'blur(0px)';
                });
        },
    });

    var card = document.querySelector('#kt_calendar .card');
    var cardHeader = card.querySelector('.card-header');
    var cardBody = card.querySelector('.card-body');

    cardBody.style.display = 'block';
    calendar.render();

    cardHeader.style.cursor = 'pointer';
    cardHeader.addEventListener('click', function () {
        if (cardBody.style.display === 'none') {
            cardBody.style.display = 'block';
            calendar.render();
        } else {
            cardBody.style.display = 'none';
            calendar.destroy();
        }
    });
});

"use strict";
var calendar, popoverInstance;
var eventDataTemplate = {
    id: "",
    eventName: "",
    eventDescription: "",
    eventLocation: "",
    startDate: "",
    endDate: "",
    allDay: false
};

var isPopoverShown = false;

const showPopover = (event) => {
    hidePopover();
    const startDateFormatted = eventDataTemplate.startDate ?
        (eventDataTemplate.allDay ? moment(eventDataTemplate.startDate).format("Do MMM, YYYY") : moment(eventDataTemplate.startDate).format("Do MMM, YYYY - h:mm a")) :
        "Invalid date";

    const endDateFormatted = eventDataTemplate.endDate ?
        (eventDataTemplate.allDay ? moment(eventDataTemplate.endDate).format("Do MMM, YYYY") : moment(eventDataTemplate.endDate).format("Do MMM, YYYY - h:mm a")) :
        "No end date"; // Biti� tarihi yoksa daha anlaml� bir mesaj g�ster

    var popoverOptions = {
        container: "body",
        trigger: "manual",
        boundary: "window",
        placement: "auto",
        dismiss: true,
        html: true,
        title: " ",
        content: `
            <div class="fw-bolder mb-2 mt-3">${eventDataTemplate.eventName}</div>
            <div class="fs-7"><span class="fw-bold">Start:</span> ${startDateFormatted}</div>
            <div class="fs-7 mb-4"><span class="fw-bold">End:</span> ${endDateFormatted}</div>
        `
    };

    popoverInstance = KTApp.initBootstrapPopover(event, popoverOptions);
    popoverInstance.show();
    isPopoverShown = true;
};

const hidePopover = () => {
    if (isPopoverShown) {
        popoverInstance.dispose();
        isPopoverShown = false;
    }
};

