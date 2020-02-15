var id = null;
var timerLongTouch;
// Long touch flag for preventing "normal touch event" trigger when long touch ends
var longTouch = false;

$(document).on("contextmenu", "table tr", function (e) {
    id = $(this).children('td:first').text().trim();

    $("#myContextMenu").css({
        display: "block",
        left: e.pageX,
        top: e.pageY
    });
    return false;
});


$("table tr").bind("touchstart", function (e) {

    timerLongTouch = setTimeout(function () {
        showContextMenu(e)  
    }, 1000);
    return false;
}).bind("touchend", function () {
    event.preventDefault();
    clearTimeout(timerLongTouch);

    if (longTouch) {
        longTouch = false;
    } else {
        $("#myContextMenu").hide();
    }
});

function showContextMenu(e) {
    longTouch = true;
    id = $(e.currentTarget).children('td:first').text().trim();

    $("#myContextMenu").css({
        display: "block",
        left: e.originalEvent.touches[0].pageX,
        top: e.originalEvent.touches[0].pageY
    });
}


$(document).on("click", ".dropdown-menu li a", function (e) {
    var href = $(this).attr("href");
    $(this).attr("href", href + "/" + id);
});

$(document).on("click", "html", function () {
    $("#myContextMenu").hide();
});