var blogContext = (function() {
    var comments = [],
        recent = 0;

    return {
        addComment: function (comment, callback) {
            $.ajax({
                url: 'AddComment',
                data: comment,
                dataType: 'json',
                type: 'POST',
                success: function (newComments) {
                    comments = comments.concat(newComments);
                    recent = newComments.length;

                    callback(comments, recent);
                },
                error: function (err) {
                    var a = err;
                }
            });
        },
        loadComments: function(search) {
            $.ajax({
                url: 'AddComment',
                data: comment,
                dataType: 'json',
                type: 'POST',
                success: function (newComments) {
                    comments = comments.concat(newComments);
                    recent = newComments.length;

                    callback(comments, recent);
                },
                error: function (err) {
                    var a = err;
                }
            });
        },
        getComments: function() {
            return comments;
        },
        getRecentAmount: function() {
            return recent;
        }
    }

    function postNewComment(event) {
        var comment = {
            'userName': $('#userName').val(),
            'date': $('#date').val(),
            'gender': $('#gender').val(),
            'text': $('#text').val()
        }

        
    }
})