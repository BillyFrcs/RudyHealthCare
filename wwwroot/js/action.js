$(document).ready(function () {
    $('form').on('submit', function () {
        setTimeout(function () {
            $('.text-danger').fadeOut('slow', function () {
                $(this).remove();
            });
        }, 5000);
    });
});

function redirectToMedicalRecords(id) {
    window.location.href = `MedicalRecords/${id}`;
}