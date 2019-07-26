function deleteConfirmation(id, controller, actionName) {

    var modalConfirm = function (callback) {

        $("#mi-modal").modal('show');

        $("#modal-btn-si").on("click", function () {
            callback(true);
            $("#mi-modal").modal('hide');
        });

        $("#modal-btn-no").on("click", function () {
            callback(false);
            $("#mi-modal").modal('hide');
        });
    };

    modalConfirm(function (confirm) {
        if (confirm) {
            location.href = `/${controller}/${actionName}/` + id;
        } else {
        }
    });
};
