// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(document).ready(function () {

    $('#sidebarCollapse').on('click', function () {
        $('#sidebar').toggleClass('active');
    });

});

//TODO: pokazywanie szczegółów użytkownika po zalogowaniu
function ShowUserDetails() {
    document.getElementById("user-details").style.visibility = "visible";
}
function HideUserDetails() {
    document.getElementById("user-details").style.visibility = "hidden";
}



function ShowLoginPartial() {
    alert("ok");
    document.getElementById("login-menu").style.visibility = "visible";
}