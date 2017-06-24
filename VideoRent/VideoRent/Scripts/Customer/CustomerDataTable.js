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
                        return "<a href='/customers/edit/" + customer.id + "'>" + customer.name + "</a>";
                    }
                },
                {
                    data: "membershipType",
                    render: function (data) {
                        return data.name;
                    }
                },
                {
                    data: "id",
                    orderable: false,
                    render: function (data) {
                        return "<button class='btn-link js-delete' data-customer-id =" + data + ">Delete</button>";
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