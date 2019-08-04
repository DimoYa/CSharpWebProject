function getFilteredData() {

    var count = $('.table').children('tbody').children('tr:first-child').children('td').length;
    var input, filter, table, tr, td, i;
    input = document.getElementById("search_input_all");
    filter = input.value.toLowerCase();
    if (input.value != '' && input.value.length >= 2) {
        table = document.getElementById("table-id");
        tr = table.getElementsByTagName("tr");
        $('#paginationDiv').hide();
        $('#maxRows').hide();
        $('#paginationRowsCount').hide();

        for (i = 1; i < tr.length; i++) {

            let flag = 0;

            for (j = 0; j < count; j++) {
                td = tr[i].getElementsByTagName("td")[j];
                if (td) {

                    var td_text = td.innerHTML;
                    if (td.innerHTML.toLowerCase().indexOf(filter) > -1) {
                        flag = 1;
                    }
                }
            }
            if (flag == 1) {
                tr[i].style.display = "";
            } else {
                tr[i].style.display = "none";
            }
        }
    } else {
        $('#maxRows').trigger('change');
        $('#paginationDiv').show();
        $('#maxRows').show();
        $('#paginationRowsCount').show();
    }
}
