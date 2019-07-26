$(document).ready(function () {
    $(".collapse.show").each(function () {
        $(this).prev(".panel-heading").find(".far").addClass("fa-object-group").removeClass("fa-object-ungroup");
    });

    $(".collapse").on('show.bs.collapse', function () {
        $(this).prev(".panel-heading").find(".far").removeClass("fa-object-ungroup").addClass("fa-object-group");
    }).on('hide.bs.collapse', function () {
        $(this).prev(".panel-heading").find(".far").removeClass("fa-object-group").addClass("fa-object-ungroup");
    });
});