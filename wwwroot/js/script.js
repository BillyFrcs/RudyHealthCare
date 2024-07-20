$(document).ready(function () {
    $('#RegistrationForms').on('submit', function (event) {
        event.preventDefault();

        var form = $(this);

        if ($(this).valid()) {
            $.ajax({
                type: form.attr('method'),
                url: form.attr('action'),
                data: form.serialize(),
                success: function () {
                    $('#SuccessConfirmation').modal('show');
                    form[0].reset();
                },
                error: function (xhr, status, error) {
                    // Handle the error
                    console.log(error);
                }
            });

            // $('#SuccessConfirmation').modal('show');
        } else {
            $(this).find('.text-danger').show();

            setTimeout(function () {
                $('.text-danger').fadeOut('slow', function () {
                    $(this).remove();
                });
            }, 5000);
        }
    });

    $('#CloseModal').on('click', function () {
        window.location.href = "/Home";
    });
});

/*
$(document).ready(function () {
    $('#RegistrationForms').on('submit', function (event) {
        event.preventDefault();
        var form = $(this);

        $.ajax({
            type: form.attr('method'),
            url: form.attr('action'),
            data: form.serialize(),
            success: function () {
                $('#SuccessConfirmation').modal('show');
                form[0].reset();
            },
            error: function (xhr, status, error) {
                // Handle the error
                console.log(error);
            }
        });
    });

    $('#CloseModal').on('click', function () {
        window.location.href = "/Home";
    });
});

$(document).ready(function () {
    $('form').on('submit', function () {
        setTimeout(function () {
            $('.text-danger').fadeOut('slow', function () {
                $(this).remove();
            });
        }, 5000);
    });
});
*/