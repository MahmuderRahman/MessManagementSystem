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
        $('#costMealForm')[0].reset();
    },

    LoadMonthDDL: function () {

        var serviceUrl = '/CostMeal/GetMonthInfo/';
        var jsonParam = '';
        AjaxManager.SendJson(serviceUrl, jsonParam, onSuccess, onFailed);
        function onSuccess(jsonData) {
            AjaxManager.populateCombo('monthDDL', jsonData, "Select Month Name");
        }
        function onFailed() {
        }
    },

    LoadYearDDL: function () {

        var serviceUrl = '/CostMeal/GetYearInfo/';
        var jsonParam = '';
        AjaxManager.SendJson(serviceUrl, jsonParam, onSuccess, onFailed);
        function onSuccess(jsonData) {
            AjaxManager.populateCombo('yearDDL', jsonData, "Select Year Name");
        }
        function onFailed() {
        }
    },

    MonthlyCalculation: function () {
        //Prompt()....Message.
        if (Message.Prompt()) {
            var jsonParam = $('#costMealForm').serialize();
            var serviceURL = "/CostMeal/MonthlyCalculation/";
            AjaxManager.SendJson(serviceURL, jsonParam, onSuccess, onFailed);
        }


        function onSuccess(JsonData) {

            if (JsonData =="200") {
                Manager.ResetForm();
                //Message.Success("save");
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
        var serviceURL = "/CostMeal/GetCostMealInfo/";
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
                        data: 'MonthName',
                        name: 'MonthName',
                        title: 'MonthName',
                        width: 100
                    },

                    {
                        data: 'YearName',
                        name: 'YearName',
                        title: 'YearName',
                        width: 100
                    },

                    {
                        data: 'TotalMeal',
                        name: 'TotalMeal',
                        title: 'TotalMeal',
                        width: 60
                    },

                    {
                        data: 'TotalAmount',
                        name: 'TotalAmount',
                        title: 'TotalAmount',
                        width: 60
                    },
                    {
                        data: 'MealRate',
                        name: 'MealRate',
                        title: 'MealRate',
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
