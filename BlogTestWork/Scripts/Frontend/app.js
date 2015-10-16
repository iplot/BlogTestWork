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
            },
            formData = new FormData(),
            chooseAvatar = $('#chooseAvatar')[0];

        //Validation
        $('#errorArea').empty();
        if (!validator(viewErrors)) {
            return;
        }
        //----------------

        formData.append('obj', JSON.stringify(comment));

        if (chooseAvatar.files && chooseAvatar.files[0]) {
            formData.append('file1', chooseAvatar.files[0]);
        }

        $.ajax({
            url: '../Home/AddComment',
//            data: { obj: JSON.stringify(comment) },
            data: formData,
            processData: false,
            contentType: false,

            dataType: 'json',
            type: 'POST',
            success: function(data) {
                viewRecentComments(data);
            },
            error: function(err) {
                viewErrors(err.statusText);
            }
        });
    }

    function updateComments() {
        var search = $('#search').val(),
            urlString,
            param;

        if (search == '') {
            urlString = '../Home/GetRecentComments';
            param = { date: JSON.stringify({ 'LastDate': lastDate }) };
        } else {
            urlString = '../Home/SearchComments';
            param = { 'search': search };
        }

        $.ajax({
            url: urlString,
            data: param,
            type: 'GET',
            dataType: 'json',
            success: function(data) {
                if (urlString === '../Home/GetRecentComments') {
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
                '<img src="../Home/LoadAvatar?userName=' + comments[i].UserName +'" width="20" height="20"/>' +
                '<p>' + comments[i].UserName + ' commented: ' + comments[i].Text + '</p>' +
                '<small>Posted ' + comments[i].Date + '</small>' +
                '<hr/>' +
                '</div>';
        }

        commentsArea.prepend(html);
    }

    function viewErrors(errors) {
        var errorList = errors.split('.'),
            i,
            length = errorList.length,
            errorArea = $('#errorArea');


        for (i = 0; i < length; i++) {
            errorArea.append('<p>' +errorList[i] + '</p>');
        }
    }
})();