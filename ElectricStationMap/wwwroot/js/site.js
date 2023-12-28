$(document).ready(function () {
    jQueryModalGet = (url, title) => {
        try {
            $.ajax({
                type: 'GET',
                url: url,
                contentType: false,
                processData: false,
                success: function (res) {
                    $('#form-modal .modal-body').html(res.html);
                    $('#form-modal .modal-title').html(title);
                    $('#form-modal').modal('show');
                },
                error: function (err) {
                    console.log(err)
                }
            })
            return false;
        } catch (ex) {
            console.log(ex)
        }
    }


    jQueryModalPost = form => {
        try {
            $.ajax({
                type: 'POST',
                url: form.action,
                data: new FormData(form),
                contentType: false,
                processData: false,
                success: function (res) {
                    if (res.isValid) {
                        $('#viewRequests').html(res.html);
                        $('#form-modal').modal('hide');
                    }
                },
                error: function (err) {
                    console.log(err)
                }
            })
            return false;
        } catch (ex) {
            console.log(ex)
        }
    }

    jQueryModalPostArray = (form, array) => {
        try {
            $.ajax({
                type: 'POST',
                url: form.action,
                data: JSON.stringify(array),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                processData: false,
                success: function (res) {
                    if (res.isValid) {
                        $('#viewRequests').html(res.html);
                        $('#form-modal').modal('hide');
                    }
                },
                error: function (err) {
                    console.log(err)
                }
            })
            return false;
        } catch (ex) {
            console.log(ex)
        }
    }


    jQueryModalDelete = form => {
        if (confirm('Вы хотите удалить запись?')) {
            try {
                $.ajax({
                    type: 'POST',
                    url: form.action,
                    data: new FormData(form),
                    contentType: false,
                    processData: false,
                    success: function (res) {
                        $('#viewRequests').html(res.html);
                    },
                    error: function (err) {
                        console.log(err)
                    }
                })
            } catch (ex) {
                console.log(ex)
            }
        }
        return false;
    }

    jQueryModalDeleteWithoutConfirmation = form => {
           try {
               $.ajax({
                   type: 'POST',
                   url: form.action,
                   data: new FormData(form),
                   contentType: false,
                   processData: false,
                   success: function (res) {
                       $('#viewRequests').html(res.html);
                   },
                   error: function (err) {
                       console.log(err)
                   }
               })
           } catch (ex) {
               console.log(ex)
           }

        return false;
    }
});