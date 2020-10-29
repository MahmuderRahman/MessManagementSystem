var _id = null;
var dTable = null;
$(document).ready(function () {

    Manager.GetDataForTable(0);
    Manager.LoadMemberDDL();

});

var Manager = {

    ResetForm: function () {
        _id = null;
        $('#mealEntryForm')[0].reset();
    },

    LoadMemberDDL: function () {

        var serviceUrl = '/MealEntry/GetMemberInfo/';
        var jsonParam = '';
        AjaxManager.SendJson(serviceUrl, jsonParam, onSuccess, onFailed);
        function onSuccess(jsonData) {
            AjaxManager.populateCombo('nameDDL', jsonData, "Select Member Name");
        }
        function onFailed() {
        }
    },

    SaveMealEntry: function () {
        //Prompt()....Message.
        if (Message.Prompt()) {
            var jsonParam = $('#mealEntryForm').serialize();
            var serviceURL = "/MealEntry/SaveMealEntry/";
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
        var serviceURL = "/MealEntry/GetMealEntry/";
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
                    { className: "dt-center", targets: [0, 1, 2, 3, 4] }
                ],        
                columns: [

                    {
                        data: 'MemberName',
                        name: 'MemberName',
                        title: 'Member Name',
                        width: 100
                    },

                    {
                        data: 'Breakfast',
                        name: 'Breakfast',
                        title: 'Breakfast',
                        width: 100
                    },

                    {
                        data: 'Lunch',
                        name: 'Lunch',
                        title: 'Lunch',
                        width: 100
                    },

                    {
                        data: 'Dinner',
                        name: 'Dinner',
                        title: 'Dinner',
                        width: 100
                    }, {
                        data: 'TotalMeal',
                        name: 'TotalMeal',
                        title: 'Total Meal',
                        width: 40,
                        render: function (data, type, row, meta) {
                            return (row.Breakfast + row.Lunch + row.Dinner) ;
                        }
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

$(document).on('click', '#saveButton', function () {
    Manager.SaveMealEntry();
});