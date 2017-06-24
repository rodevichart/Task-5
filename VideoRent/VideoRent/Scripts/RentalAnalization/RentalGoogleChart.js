google.charts.load('current', { 'packages': ['corechart'] });
google.charts.setOnLoadCallback(drawChart);

function drawChart() {
    $.ajax({
        type: 'POST',
        dataType: 'json',
        contentType: 'application/json',
        url: 'RentVideoAnalization/GetCharData',
        data: 'data',
        success: function (response) {

            drawchart(response);
        },

        error: function () {
            bootbox.error("Error loading data! Please try again.");
        }
    });
};

function drawchart(dataValues) {
    var data = new google.visualization.DataTable();


    data.addColumn('string', 'MovieName');
    data.addColumn('number', 'Count');

    for (var i = 0; i < dataValues.length; i++) {
        data.addRow([dataValues[i].movieName, dataValues[i].count]);
    }

    var chart = new google.visualization.PieChart(document.getElementById('chartdiv'));

    chart.draw(data,
      {
          title: "Rent Movie",
          position: "top",
          fontsize: "14px",
          chartArea: { width: '50%' },
      });
}