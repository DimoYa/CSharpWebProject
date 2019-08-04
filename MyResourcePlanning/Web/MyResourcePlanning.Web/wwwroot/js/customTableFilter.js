 $.fn.dataTable.ext.search.push(
            function (settings, data, dataIndex) {
                let min = $('#min').val()
                let max = $('#max').val()
                let createdAt = data[0] || 0;
                let startDate = moment(min, "DD/MM/YYYY");
                let endDate = moment(max, "DD/MM/YYYY");
                let diffDate = moment(createdAt, "DD/MM/YYYY");
                if (
                    (min == "" || max == "") ||
                    (diffDate.isBetween(startDate, endDate))

                ) { return true; }
                return false;

            }
        );

        $(document).ready(function () {
            let table = $('#myTable').DataTable();

            $('#min, #max').change(function () {
                table.draw();
            });
        });