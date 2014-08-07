function ValidateFields() {
    
    var missingCounter = 0;
    var fields = $('.mandatory'); //Array for all textboxes
    for (var i = 0; i < fields.length; i++) {
        var value = $(fields[i]).val();
        if (value.toString().length == 0) {
            missingCounter++;
            $(fields[i]).css('border-color', 'red');
        }
        else
            $(fields[i]).css('border-color', 'gainsboro');
    }
    if (missingCounter > 0 )
        return false;
    return true;
}

function validateRadioButtons() {
    var radioButtons = $("input[type=radio]");
    var counter = -1;
    for (var i = 0; i < radioButtons.length; i++) {
        if (radioButtons[i].checked) {
            counter = i;
        }
    }
    if (counter == -1) {
        alert("please select radio");
        return false;
    } else {
        return true;
    }
}