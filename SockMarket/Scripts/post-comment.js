$("#post-button").click(function (e) {
    var commentText = $("#comment-input").val();
    if (commentText.length === 0 || !commentText.trim()) return;
    e.preventDefault();
    $.ajax({
        type: "POST",
        url: $(this).data("url"),
        contentType: "application/json; charset=utf-8",
        dataType: 'json',
        data: JSON.stringify({ text: commentText }),
        success: function (comment) {
            addNewComment(comment);
        },
        error: function () {
            alert('Unable to add comment. Please try again');
        }
    });
});

function addNewComment(comment) {
    $("#comments").prepend('\
        <div class="panel panel-default">\
            <div class="panel-heading">' + comment.Author + ' commented ' + comment.Time + '</div>\
            <div class="panel-body" style="white-space: pre-line">' + comment.Text + '</div>\
        </div>\
        ');
}