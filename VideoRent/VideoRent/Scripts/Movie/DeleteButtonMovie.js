$("#movies").on("click", ".js-delete", function () {
    var button = $(this);
    bootbox.confirm("Are you sure to delete this movie?", function (result) {
        if (result) {
            $.ajax({
                url: "/Movies/Delete/" + button.attr("data-movies-id"),
                method: "POST",
                cache: false,
                success: function () {
                    dataTable.row(button.parents("tr")).remove().draw(false);
                },
                error: function (text) {
                    bootbox.alert(text + "\nPlease refresh page and try again");
                }
            });
        }
    });
});