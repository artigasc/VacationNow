

//progress bar
function progressbar() {
    $(".progressbar").on('click', 'span', function (event) {

        var a = "#" + this.id;
        var prevstep = $(a).parent().prev().children('span').attr('id');
        if (!$(this).parent().is(':first-child')) {
            if ($("#" + prevstep).parent().hasClass("act")) {
                $(a).parent().addClass('act');
            }
        }

    });
}
