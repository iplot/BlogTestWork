(function () {
    var lastDate = null;

    $(document).ready(function() {
        $('#postBtn').on('click', postNewComment);
        $('#searchBtn').on('click', updateComments);

        updateComments();
    });

    function postNewComment() {
        var comment = {
            'userName': $('#userName').val(),
            'userDate': $('#date').val(),
            'gender': $('#gender').val(),
            'text': $('#text').val(),
            'lastDate': lastDate
        }

        $.ajax({
            url: 'Home/AddComment',
            data: { obj: JSON.stringify(comment) },
            dataType: 'json',
            type: 'POST',
            success: function(data) {
                viewRecentComments(data);
            },
            error: function(err) {
                var a = err;
            }
        });
    }

    function updateComments() {
        var search = $('#search').val(),
            urlString,
            param;

        if (search == '') {
            urlString = 'Home/GetRecentComments';
            param = { date: JSON.stringify({ 'LastDate': lastDate }) };
        } else {
            urlString = 'Home/SearchComments';
            param = { 'search': search };
        }

        $.ajax({
            url: urlString,
            data: param,
            type: 'GET',
            dataType: 'json',
            success: function(data) {
                if (urlString === 'Home/GetRecentComments') {
                    viewRecentComments(data);
                } else {
                    $('#postArea').empty();
                    viewComments(data);
                    lastDate = null;
                }
            }
        });
    }

    function viewRecentComments(data) {
        var recentCounter = $('#recentComments');

        if (lastDate === null) {
            $('#postArea').empty();
        }

        viewComments(data.Comments);

        recentCounter.text(data.Comments.length);
        lastDate = new Date(data.LastDateTime);
    }

    function viewComments(comments) {
        var commentsArea = $('#postArea'),
            
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
    }
})();