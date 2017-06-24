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
                        return "<a href='/movies/details/" + movie.id + "'>" + movie.name + "</a>";
                    }
                },
                {
                    data: "genre",
                    render: function (data) {
                        return data.name;
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