function actionConfirmation(id, controller, actionName) {

    let comment = '';

    var modalConfirm = function (callback) {

        switch (actionName) {

            case 'Approve': $("#commentLabel").text('This item will be booked');
                break;
            case 'Reject': $("#commentLabel").text('This item will be rejected');
                break;
            case 'Return': $("#commentLabel").text('This item will be returned to planner');
                break;
            case 'Lock': $("#commentLabel").text('This user will be locked'), $("#commentSection").hide();
                break;
            case 'UnLock': $("#commentLabel").text('This user will be unlocked'), $("#commentSection").hide();
                break;
            default: $("#commentSection").hide();
                break;
        }

        $("#modalWin").modal('show');

        $("#modal-btn-yes").on("click", function () {
            comment = $("#comment").val();
            callback(true);
            $("#modalWin").modal('hide');
            $("#comment").val('');
        });

        $("#modal-btn-no").on("click", function () {
            callback(false);
            $("#modalWin").modal('hide');
            $("#comment").val('');
        });
    };

    modalConfirm(function (confirm) {
        if (confirm) {
            location.href = `/${controller}/${actionName}?id=` + id + "&comment=" + comment;
        } else {
        }
    });
};
