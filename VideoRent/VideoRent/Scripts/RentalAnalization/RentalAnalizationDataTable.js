var dataTable = {
    table: null,
    initializeDataTable: function () {
        dataTable.table = $("#rentals").DataTable({
            processing: true,
            serverSide: true,
            ajax: {
                url: "/RentVideoAnalization/Index",
                type: "POST",
                dataSrc: "data"
            },
            columns: [
                {
                    data: "customer",
                    render: function (data) {
                        return data.name;
                    }
                },
                {
                    data: "movie",
                    render: function (data) {
                        return data.name;
                    }
                },
                {
                    data: "dateRented",
                    render: function (data) {
                        var options = { day: 'numeric', month: 'short', year: 'numeric' };
                        return new Date(data).toLocaleDateString('en-GB', options);

                    }
                },
                {
                    data: "dateReturned",
                    render: function (data) {
                        if (data == null)
                            return "Not yet returned";
                        return new Date(data).toLocaleDateString('en-GB', options);
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