(function() {
    $(document).ready(function() {
        $('#postBtn').on('click', postNewComment);
        $('#searchBtn').on('click', updateComments);

        updateComments();
    });

    function postNewComment() {
        var comment = {
            'userName': $('#userName').val(),
            'date': $('#date').val(),
            'gender': $('#gender').val(),
            'text': $('#text').val()
        }

        $.ajax({
            url: 'Home/AddComment',
            data: comment,
            dataType: 'json',
            type: 'POST',
            success: function(data) {
                viewComments(data);
            },
            error: function(err) {
                var a = err;
            }
        });
    }

    function updateComments() {
        var search = $('#search').val();

        $.ajax({
            url: 'Home/GetComments',
            data: { 'search': search },
            type: 'GET',
            dataType: 'json',
            success: function(data) {
                viewComments(data);
            }
        });
    }

    function viewComments(comments) {
        var commentsArea = $('#postArea'),
            recentCounter = $('#recentComments'),
            length = comments.length,
            i,
            html = '',
            date;

        for (i = 0; i < length; i++) {
            html +=
                '<div>' +
                '<p>' + comments[i].UserName + ' commented: ' + comments[i].Text + '</p>' +
                '<small>Posted ' + comments[i].Date + '</small>' +
                '<hr/>' +
                '</div>';
        }

        commentsArea.prepend(html);
        recentCounter.text(length);
    }
})();