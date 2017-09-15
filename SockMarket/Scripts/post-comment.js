$("#post-button").click(function (e) {
    var commentText = $("#comment-input").val()
    if (commentText === '') return
    e.preventDefault();
    $.ajax({
        type: "POST",
        url: $(this).data("url"),
        contentType: "application/json; charset=utf-8",
        dataType: 'json',
        data: JSON.stringify({ text: commentText }),
        success: function () {
        },
        error: function () {
            alert('Unable to add comment. Please try again');
        }
    });
});