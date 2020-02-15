$(document).ready(function () {
    showSubscriptionPopup();
});

var showSubscriptionPopup = function () {
    var value = localStorage.getItem("SBSD");
    if (value && value === "HHHHHHHHHH") {
        return 'ALREADY SUBSCRIBED';
    }
    setTimeout(function () {
        $(".tingle-modal").removeClass("tingle-modal--hidden").addClass("tingle-modal--visible");
    }, 5000);

};


var validateEmail = function (email) {
    var expr = /^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$/;
    return expr.test(email);

};

var saveSubscription = function () {
    $.post("/Subscription/SaveSubscription", { email: $("input#mce-EMAIL").val() },
        function (data) {
            if (data.success) {
                $("#mce-success-response").removeClass('hidden').html(data.message);
                $('.form-group').addClass('hidden');
                setTimeout(function () {
                    localStorage.setItem('SBSD', 'HHHHHHHHHH');
                    $(".tingle-modal").removeClass("tingle-modal--visible").addClass("tingle-modal--hidden");
                }, 2000);
            }
            else {
                $("#mce-responses #mce-success-response").addClass('hidden')
                $("#mce-responses #mce-error-response").removeClass('hidden').html(data.errorMessage);
            }
        }, "json")
};

$(document).on("click", "#mc-embedded-subscribe", function () {
    if (!validateEmail($("input#mce-EMAIL").val())) {
        $("#mce-responses #mce-error-response").removeClass('hidden').html("Please enter valid email.");
        return
    }
    saveSubscription();
});

$(document).on("click", ".tingle-modal__closeIcon", function () {
    $(".tingle-modal").removeClass("tingle-modal--visible").addClass("tingle-modal--hidden");
});
