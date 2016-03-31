$(function () {
    $('.menuzkli').hover(function () {
        $(this).find('.menuzkblock').css('display', 'block');
    }, function () {
        $(this).find('.menuzkblock').css('display', 'none');
    });
});