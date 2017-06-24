var dataTable = {
    table: null,
    initializeDataTable: function () {
        dataTable.table = $("#movies").DataTable({
            processing: true,
            serverSide: true,
            ajax: {
                url: "/Movies/Index",
                type: "POST",
                dataSrc: "data"
            },
            columns: [
                {
                    data: "name",
                    render: function (data, type, movie) {
                        return "<a href='/movies/edit/" + movie.id + "'>" + movie.name + "</a>";
                    }
                },
                {
                    data: "genre",
                    render: function (data) {
                        return data.name;
                    }
                },
                {
                    data: "id",
                    orderable: false,
                    render: function (data) {
                        return "<button class='btn-link js-delete' data-movies-id =" + data + ">Delete</button>";
                    }
                }
            ]
        });
    },
    getData: function () {
        if (dataTable.table == null)
            dataTable.initializeDataTable();
        else
            dataTable.table.ajax.reload();
    }
}

$(document).ready(function () {
    dataTable.getData();
});