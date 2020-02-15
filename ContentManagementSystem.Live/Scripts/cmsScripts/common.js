var globalErrorMessage = "An error occurred while processing your request."

function ajaxErrorBlock(jqXHR, exception) {
    if (jqXHR.status === 0) {
        bootbox.alert("Not connected to network.\n Please verify network connection.");
    }
    else if (jqXHR.status == 404) {
        bootbox.alert('Requested page not found');
    }
    else if (jqXHR.status == 500) {
        bootbox.alert('Internal Server Error');
    }
    else if (exception == "parsererror") {
        bootbox.alert('Requested JSON parse failed');
    }
    else if (exception == "timeout") {
        bootbox.alert('Request timed out');
    }
    else if (exception == "abort") {
        //bootbox.alert('Ajax request aborted');
        bootbox.alert('Request denied');
    }
    else {
        bootbox.alert("Something went wrong. Please try again");
        //bootbox.alert('Uncaught Error.\n' + jqXHR.responseText);
    }
}