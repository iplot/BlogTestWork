$(document).ready(function() {
    $('#postBtn').on('click', postNewComment);
});

function postNewComment(event) {
    var comment = {
        'userName': $('#userName').val(),
        'date': $('#date').val(),
        'gender': $('#gender').val(),
        'text': $('#text').val()
    }

    $.ajax({
        url: 'AddComment',
        data: comment,
        dataType: 'json',
        type: 'POST',
        success: function(data) {
            var test = data;
        },
        error: function(err) {
            var a = err;
        }
    });
}