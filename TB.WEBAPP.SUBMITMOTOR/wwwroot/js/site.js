// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
// This script handles the scroll to top and scroll to bottom functionality
const scrollToTopButton = document.getElementById("scrollToTopButton");
const scrollToBottomButton = document.getElementById("scrollToBottomButton");

// Ensure the buttons are hidden initially
document.addEventListener("DOMContentLoaded", () => {
    scrollFunction();
});

// When the user scrolls down 1000px from the top of the document, show the button
window.onscroll = function () { scrollFunction() };

// When the user scrolls down 1000px from the top of the document, show the button
function scrollFunction() {
    if (document.body.scrollTop > 1000 || document.documentElement.scrollTop > 1000) {
        scrollToTopButton.style.display = "block";
        scrollToBottomButton.style.display = "none";
    } else {
        scrollToTopButton.style.display = "none";
        scrollToBottomButton.style.display = "block";
    }
}
// When the user scrolls down 1000px from the top of the document, show the button
function topFunction() {
    window.scrollTo({ top: 0, behavior: 'smooth' })
};

// When the user clicks on the button, scroll to the bottom of the document
function bottomFunction() {
    document.getElementById('siteFooter').scrollIntoView({ behavior: "smooth" });
}

// When the user clicks on the button, scroll to the top of the document
scrollToTopButton.addEventListener("click", function (e) {
    e.preventDefault();
    topFunction();
});

// When the user clicks on the button, scroll to the bottom of the document
scrollToBottomButton.addEventListener('click', function (e) {
    e.preventDefault();
    bottomFunction();
});