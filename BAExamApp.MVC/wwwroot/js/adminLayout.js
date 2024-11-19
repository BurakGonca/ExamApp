window.addEventListener('DOMContentLoaded', event => {

    // Toggle the side navigation
    const sidebarToggle = document.body.querySelector('#sidebarToggle');
    if (sidebarToggle) {
        // Uncomment Below to persist sidebar toggle between refreshes
        // if (localStorage.getItem('sb|sidebar-toggle') === 'true') {
        //     document.body.classList.toggle('sb-sidenav-toggled');
        // }
        sidebarToggle.addEventListener('click', event => {
            event.preventDefault();
            document.body.classList.toggle('sb-sidenav-toggled');
            localStorage.setItem('sb|sidebar-toggle', document.body.classList.contains('sb-sidenav-toggled'));
        });
    }

});


const visibleitemlogo = document.getElementById("layoutSidenav_nav");
const visiblemenu = document.querySelectorAll('.linkcontent');
/*const visiblemenu = document.querySelectorAll('.collapse ');*/

const navElement = document.querySelector('.nav');

function removeShowClassFromNav() {
    const showDivs = navElement.querySelectorAll('.show');
    showDivs.forEach((divElement) => {
        divElement.classList.remove('show');
    });
}

function findShowClassFromNav() {
    return navElement.querySelectorAll('.show');
}

visibleitemlogo.addEventListener("mouseenter", () => {
    visiblemenu.forEach((divElement) => {
        divElement.classList.remove('linkcontentvisible');
    });
});


visibleitemlogo.addEventListener("mouseleave", () => {
    visiblemenu.forEach((divElement) => {
        divElement.classList.add('linkcontentvisible');
    });

    const showDivs = findShowClassFromNav();
    showDivs.forEach((divElement) => {
        divElement.classList.remove('show');
    });
});

$(document).ready(function () {
    $('[data-bs-toggle="tooltip"]').tooltip();
});