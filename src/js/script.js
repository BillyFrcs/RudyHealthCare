document.addEventListener("DOMContentLoaded", function () {
    const dateInput = document.getElementById('registrationDate');

    // Update class based on the input value
    function updatePlaceholder() {
        if (dateInput.value) {
            dateInput.classList.add('filled');
        } else {
            dateInput.classList.remove('filled');
        }
    }

    dateInput.addEventListener('change', updatePlaceholder);
    dateInput.addEventListener('input', updatePlaceholder);

    // Initialize placeholder state
    updatePlaceholder();
});