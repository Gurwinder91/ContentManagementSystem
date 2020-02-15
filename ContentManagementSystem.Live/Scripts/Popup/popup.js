var popup, index = -1, draggable = [], closeOnEscs = [];

var showPopup = function (content) {
    if (!arguments.length  || arguments.length > 1) {
        console.error('Supplied argument doesn\'t match the function definition');
    } else {
        var contentHtml, drag = false, title = '', showTitle = false, titleBackground = '', showClose = true, overlayColor = '', closeOnEsc = true;
        if (typeof content === 'object') {
            var allProps = ['content', 'drag', 'title', 'showTitle', 'titleBackground', 'showClose', 'overlayColor', 'closeOnEsc'];
            var pass = true;
            for (var prop in content) {
                if (allProps.indexOf(prop) === -1) {
                    console.error('Invalid structure of supplied argument');
                    return false;
                }
            }
            contentHtml = content.content || "";
            drag = content.drag || false;
            title = content.title || "";
            showTitle = content.showTitle || false;
            titleBackground = content.titleBackground || '#F16363';
            showClose = content.showClose || false;
            overlayColor = content.overlayColor || "rgba(0,0,0,0.4)";
            closeOnEsc = content.closeOnEsc || false;
        }
        else {
            contentHtml = content;
        }
        if (typeof contentHtml !== 'string') {
            console.error('contentHtml should be string');
        }
        else if (typeof drag !== 'boolean') {
            console.error('drag should be boolean');
        }
        else if (typeof title !== 'string') {
            console.error('title should be string always');
        }
        else if (typeof showTitle !== 'boolean') {
            console.error('showTitle should be boolean');
        }
        else if (typeof titleBackground !== "string") {
            console.error('titleBackground should be string hexacolor/rgb/rgba');
        }
            //else if (!overlayColor || typeof overlayColor !== 'string' || !/[R][G][B][A]?[(]([01]?[0-9]?[0-9]|2[0-4][0-9]|25[0-5])\s*,\s*([01]?[0-9]?[0-9]|2[0-4][0-9]|25[0-5])\s*,\s*([01]?[0-9]?[0-9]|2[0-4][0-9]|25[0-5])[)]/i.test(overlayColor)) {
            //    console.log('overlayColor should be rgb(a)');
            //}
        else if (typeof closeOnEsc !== 'boolean') {
            console.log('closeOnEsc should be boolean');
        }
        else {
            index++;
            var overlayId = "overlay" + index;
            var popupId = "popup" + index;
            var contentId = "content" + index;

            var html = '<div id="' + overlayId + '" class="overlay">' +
                '<div id="' + popupId + '" class="popup">' +
                '    <div id="' + contentId + '" class="pop-content">' +
                '   </div>' +
                 '</div>' +
            '</div>';

            document.body.innerHTML += html;
            document.getElementById(contentId).innerHTML = contentHtml;
            document.getElementById(contentId).style.transition = "all 2s";
            var titleId = "";
            if (showTitle) {
                titleId = 'title' + index;
                var titleHtml = '<div class="popup-title" id="' + titleId + '">'
                    + ((showClose) ? ('<button type="button" class="close hp" aria-label="Cancel"><span aria-hidden="true">×</span></button>') : '')
                    + '<h4>' + title + '</h4></div>';
                document.getElementById(contentId).insertAdjacentHTML('afterbegin', titleHtml);
                //if (titleBackground) document.getElementById(contentId).style.border = '1px solid ' + titleBackground;
                //document.getElementById(contentId).getElementsByClassName('popup-title')[0].style.background = titleBackground;
            }
            else if (!showTitle && showClose) {
                var closeHtml = '<button type="button" class="close hp" aria-label="Cancel"><span aria-hidden="true">×</span></button>';
                document.getElementById(contentId).insertAdjacentHTML('afterbegin', closeHtml);
            }
            draggable.push(drag);
            closeOnEscs.push(closeOnEsc);
            if (overlayColor) document.getElementById(overlayId).style.background = overlayColor;

            enableDrag(popupId, titleId);
        }
    }
};

var mouseDown = function (event) {
    var left = parseInt(window.getComputedStyle(popup).getPropertyValue("left"));
    var top = parseInt(window.getComputedStyle(popup).getPropertyValue("top"));
    var mouseX = event.clientX;
    var mouseY = event.clientY;

    document.onmousemove = function (e) {
        var dx = mouseX - e.clientX;
        var dy = mouseY - e.clientY;

        popup.style.left = left - dx + "px";
        popup.style.top = top - dy + "px";
    };
}

var mouseUp = function (event) {
    document.onmousemove = null;
}

var hidePopup = function (callback) {
    document.getElementById("overlay" + index).remove();
    draggable.splice(index, 1);
    index--;
    if (index > -1)
        enableDrag("popup" + (index));
    if (callback)
        callback();
}

var cancel = function (e) {
    document.getElementById("overlay" + index).remove();
    draggable.splice(index, 1);
    index--;
    if (index > -1)
        enableDrag('popup' + (index), 'title' + (index));
}

var enableDrag = function (popupId, titleId) {
    popup = document.getElementById(popupId);
    if (popup) {
        if (draggable[index]) {
            var title = document.getElementById(titleId);
            if (title) {
                title.onmousedown = mouseDown;
                title.onmouseup = mouseUp;
                title.classList.add('drag');
            }
            else {
                popup.onmousedown = mouseDown;
                popup.onmouseup = mouseUp;
                popup.classList.add('drag');
            }
        }
        var closeButton = popup.getElementsByClassName("close hp").length;
        if (closeButton)
            popup.getElementsByClassName("close hp")[0].onclick = cancel;
    }
}

document.onkeyup = function (e) {
    if (document.getElementById('overlay' + index) && closeOnEscs[index]) {
        if (e.keyCode === 27 || e.charCode === 27)
            cancel(e);
    }
}