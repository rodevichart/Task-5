﻿$(document).ready(function () {

    var vm = {
        movieIds: []
    };

    var customers = new Bloodhound({
        datumTokenizer: Bloodhound.tokenizers.obj.whitespace('name'),
        queryTokenizer: Bloodhound.tokenizers.whitespace,
        remote: {
            url: '/getcustomers?query=%QUERY',
            wildcard: '%QUERY'
        }
    });

    $('#customer').typeahead({
        minLength: 3,
        highlight: true
    }, {
        name: 'customers',
        display: 'name',
        source: customers
    }).on("typeahead:select", function (e, customer) {
        vm.customerId = customer.id;
    });


    var movies = new Bloodhound({
        datumTokenizer: Bloodhound.tokenizers.obj.whitespace('name'),
        queryTokenizer: Bloodhound.tokenizers.whitespace,
        remote: {
            url: '/getmovies?query=%QUERY',
            wildcard: '%QUERY'
        }
    });

    $('#movie').typeahead({
        minLength: 3,
        highlight: true,
        autoselect: true
    }, {
        name: 'movies',
        display: 'name',
        source: movies
    }).on("typeahead:select", function (e, movie) {
        $("#movies").append("<li class='list-group-item'>" + movie.name + "</li>");

        $("#movie").typeahead("val", "");

        vm.movieIds.push(movie.id);
    });


    $.validator.addMethod("validCustomer", function () {
        return vm.customerId && vm.customerId !== 0;
    }, "Please select a valid customer.");


    $.validator.addMethod("atLeastOneMovie", function () {
        return vm.movieIds > 0;
    }, "Please select at least one movie.");



    var validator = $("#newRental").validate({
        submitHandler: function () {
            $.ajax({
                url: "/api/newRental",
                method: "POST",
                data: vm
            })
                .done(function () {
                    toastr.success("Rentals Successfully recoreded.");
                    $("#customer").typeahead("val", "");
                    $("#movie").typeahead("val", "");
                    $("#movies").empty();

                    vm = { moviesIds: [] };

                    validator.resetForm();
                })

                .fail(function () {
                    toastr.error("Something unexpected happened.");
                });
            return false;
        }

    });
})