var dataTable = {
    table: null,
    initializeDataTable: function () {
        dataTable.table = $("#customers").DataTable({
            processing: true,
            serverSide: true,
            ajax: {
                url: "/Customers/Index",
                type: "POST",
                dataSrc: "data"
            },
            columns: [
                {
                    data: "name",
                    render: function (data, type, customer) {
                        return "<a href='/customers/Details/" + customer.id + "'>" + customer.name + "</a>";
                    }
                },
                {
                    data: "membershipType",
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