function actionConfirmation(id, controller, actionName) {

    var comment = '';

    var modalConfirm = function (callback) {

        if (actionName != 'Reject' || actionName != 'Return') {
            $("#commentSection").hide();
        }

        $("#modalWin").modal('show');

        $("#modal-btn-yes").on("click", function () {
            comment = $("#comment").val();
            callback(true);
            $("#modalWin").modal('hide');
        });

        $("#modal-btn-no").on("click", function () {
            callback(false);
            $("#modalWin").modal('hide');
        });
    };

    modalConfirm(function (confirm) {
        if (confirm) {
            location.href = `/${controller}/${actionName}?id=` + id + "&comment=" + comment;
        } else {
        }
    });
};
