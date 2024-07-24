document.getElementById("add-social-icon-btn").addEventListener("click", function () {
    const descInput = document.getElementById("social-icon-desc");
    const urlInput = document.getElementById("social-icon-url");
    const form = document.getElementById("add-social-icon-form");
    const dynamicButton = document.getElementById("dynamic-button");
    const addTexts = document.querySelectorAll(".add-text1");

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
});
