


app.init.add("toggler", function () {

    $('#chkTOGGLE').on('change', function () {
        var checked = $(this).is(":checked");
        $('#lblTOGGLE').text(checked ? "de-select all" : "select all");
        $('#GRID tbody input[type="checkbox"]').each(function () { $(this).prop('checked', checked); });
    });

});

