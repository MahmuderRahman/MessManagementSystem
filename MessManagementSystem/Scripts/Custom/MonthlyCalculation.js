var _id = null;
var dTable = null;
$(document).ready(function () {

    Manager.GetDataForTable(0);
    Manager.LoadMonthDDL();
    Manager.LoadYearDDL();

    //$('#processButton').click(function () {
    //    Manager.MonthlyCalculation();
    //});

});

var Manager = {

    ResetForm: function () {
        _id = null;
        $('#monthlyCalculationForm')[0].reset();
    },

    LoadMonthDDL: function () {

        var serviceUrl = '/MonthlyCalculation/GetMonthInfo/';
        var jsonParam = '';
        AjaxManager.SendJson(serviceUrl, jsonParam, onSuccess, onFailed);
        function onSuccess(jsonData) {
            AjaxManager.populateCombo('monthDDL', jsonData, "Select Month Name");
        }
        function onFailed() {
        }
    },

    LoadYearDDL: function () {

        var serviceUrl = '/MonthlyCalculation/GetYearInfo/';
        var jsonParam = '';
        AjaxManager.SendJson(serviceUrl, jsonParam, onSuccess, onFailed);
        function onSuccess(jsonData) {
            AjaxManager.populateCombo('yearDDL', jsonData, "Select Year Name");
        }
        function onFailed() {
        }
    },

    MonthlyCalculation: function () {
        if (Message.Prompt()) {
            var jsonParam = {
                monthId: $('#monthDDL').val(),
                yearId: $('#yearDDL').val(),
                date: $('#date').val()
            };
            var serviceURL = "/MonthlyCalculation/MonthlyCalculation/";
            AjaxManager.SendJson(serviceURL, jsonParam, onSuccess, onFailed);
        }


        function onSuccess(JsonData) {

            if (JsonData == "200") {
                Manager.ResetForm();
                Message.Success("save");
                Manager.GetDataForTable(1);
            }
            else {
                Message.Warning(JsonData);
            }
        }

        function onFailed(xhr, status, err) {
            Message.Exception(xhr);
        }
    },

    GetDataForTable: function (refresh) {
        var jsonParam = '';
        var serviceURL = "/MonthlyCalculation/GetMonthlyCalculationInfo/";
        AjaxManager.SendJsonAsyncON(serviceURL, jsonParam, onSuccess, onFailed);
        function onSuccess(jsonData) {
            Manager.LoadDataTable(jsonData, refresh);
        }

        function onFailed(xhr, status, err) {
            Message.Exception(xhr);
        }
    },

    LoadDataTable: function (data, refresh) {
        if (refresh == "0") {
            dTable = $('#tableElement').DataTable({
                lengthMenu: [[5, 10, 15, 20], [5, 10, 15, 20, "All"]],
                columnDefs: [
                    { visible: false, targets: [] },
                    { className: "dt-center", targets: [0, 1, 2, 3, 4, 5] }
                ],
                columns: [

                    {
                        data: 'YearName',
                        name: 'YearName',
                        title: 'Year',
                        width: 100
                    },

                    {
                        data: 'Name',
                        name: 'Name',
                        title: 'Month',
                        width: 100
                    },

                    {
                        data: 'TotalMeal',
                        name: 'TotalMeal',
                        title: 'Total Meal',
                        width: 60
                    },

                    {
                        data: 'TotalCost',
                        name: 'TotalCost',
                        title: 'Total Cost',
                        width: 60
                    },
                    {
                        data: 'MealRate',
                        name: 'MealRate',
                        title: 'Meal Rate',
                        width: 50
                    },


                    {
                        data: 'Date',
                        name: 'Date',
                        title: 'Date',
                        width: 80,
                        render: function (cellValue, type, row) {
                            return Manager.FormatDateToDayMonthYear(cellValue);
                        }
                    },

                ],
                data: data
            });
        } else {
            dTable.clear().rows.add(data).draw();
        }
    },

    FormatDateToDayMonthYear: function (date) {
        var monthNames = ["Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"];
        var dateObject = new Date(date);
        var day = ("0" + dateObject.getDate()).slice(-2);
        var month = dateObject.getMonth();

        return (day) + "-" + (monthNames[month]) + "-" + dateObject.getFullYear();
    },

    FormatDateToYearMonthDay: function (date) {
        var dateObject = new Date(date);
        var day = ("0" + dateObject.getDate()).slice(-2);
        var month = ("0" + (dateObject.getMonth() + 1)).slice(-2);

        return dateObject.getFullYear() + "-" + (month) + "-" + (day);
    }
}
$(document).on('click', '#processButton', function () {
    Manager.MonthlyCalculation();
});
