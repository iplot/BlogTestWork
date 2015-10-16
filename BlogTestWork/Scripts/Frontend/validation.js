var validator = function Validate(callback) {
    var userName = $('#userName').val(),
        date = $('#date').val(),
        text = $('#text').val(),
        errorText = '';

    if (!Validate.prototype.expression(userName)) {
        errorText += 'UserName field must contains at least one letter or number.';
    }

    if (!Validate.prototype.futureDate(date)) {
        errorText += ' Date unspecified or you try post from future.';
    }

    if (text == '' || text == ' ') {
        errorText += 'Text field is required';
    }

    if (errorText !==  '') {
        callback(errorText);
        return false;
    }

    return true;
}

validator.prototype.expression = function(val) {
    var regEx = /[A-Za-z0-9]/;
    return regEx.test(val);
}

validator.prototype.futureDate = function(val) {
    var now = new Date,
        inputDate = Date.parse(val);
    return val && inputDate <= now;
}