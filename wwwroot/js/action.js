$(document).ready(function () {
    $('form').on('submit', function () {
        setTimeout(function () {
            $('.text-danger').fadeOut('slow', function () {
                $(this).remove();
            });
        }, 5000);
    });
});

$('#DeleteConfirmationModal').on('show.bs.modal', function (event) {
    var button = $(event.relatedTarget);
    var id = button.data('id');
    var modal = $(this);

    // console.log(id);

    modal.find('.modal-body').text('Apakah anda yakin ingin menghapus data ini?');
    modal.find('#DeleteId').val(id);

   //  $('#DeleteForm').attr('action', `/Admin/MedicalRecords/Delete/${id}`);
});

function redirectToMedicalRecords(id) {
    window.location.href = `MedicalRecords/${id}`;
}