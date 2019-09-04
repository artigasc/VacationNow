$(function () {
    $('#sidebar-toggler').on('click', function () {
        $('#sidebar').toggleClass('active');
    });
});
$(".rotate").click(function () {
    $(this).toggleClass('active');
});

$('.custom-file input').change(function (e) {
    $(this).next('.custom-file-label').html(e.target.files[0].name);
});
/**
 * reset a form
 * include type input: text, password, checkbox, select, texarea, file
 * /
 * @method clearForm
 * @param id_form {String} The Id of the form
 */
function clearForm(id_form) {
    $("#" + id_form).find('input[type="text"]').val('');
    $("#" + id_form).find('input[type="password"]').val('');
    $("#" + id_form).find('input[type="checkbox"]').removeAttr("checked");
    $("#" + id_form).find('select option').removeAttr("selected");
    $("#" + id_form).find('textarea').val('');
    $("#" + id_form).find('input[type="file"]').val('');
}