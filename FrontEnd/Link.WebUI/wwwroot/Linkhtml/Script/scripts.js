document.addEventListener('DOMContentLoaded', function () {
    // Social Icon Toggle Functionality
    function toggleSocialIconForm() {
        const descInput = document.getElementById("social-icon-desc");
        const urlInput = document.getElementById("social-icon-url");
        const form = document.getElementById("add-social-icon-form");
        const dynamicButton = document.getElementById("dynamic-button");
        const addTexts = document.querySelectorAll(".add-text1");

        if (descInput && urlInput && form && dynamicButton) {
            // Toggle visibility of inputs, form and text spans
            if (descInput.hidden && urlInput.hidden) {
                descInput.hidden = false;
                urlInput.hidden = false;
                form.classList.remove("hidden");
                dynamicButton.classList.remove("hidden");
                addTexts.forEach(text => {
                    text.hidden = false;
                });
            } else {
                descInput.hidden = true;
                urlInput.hidden = true;
                form.classList.add("hidden");
                dynamicButton.classList.add("hidden");
                addTexts.forEach(text => {
                    text.hidden = true;
                });
            }
        }
    }

    // Initialize Event Listeners
    function initializeEventListeners() {
        const addSocialIconBtn = document.getElementById("add-social-icon-btn");
        const menuToggle = document.getElementById('menu-toggle');
        const dropdownMenu = document.getElementById('dropdown-menu');

        if (addSocialIconBtn) {
            addSocialIconBtn.addEventListener("click", toggleSocialIconForm);
        }

        if (menuToggle && dropdownMenu) {
            menuToggle.addEventListener('click', function (e) {
                e.preventDefault();
                dropdownMenu.classList.toggle('show');
            });

            document.addEventListener('click', function (e) {
                if (!menuToggle.contains(e.target) && !dropdownMenu.contains(e.target)) {
                    dropdownMenu.classList.remove('show');
                }
            });
        }
    }

    // Initialize everything after DOM is fully loaded
    initializeEventListeners();
});
