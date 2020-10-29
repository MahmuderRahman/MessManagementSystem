var _id = null;
var dTable = null;
$(document).ready(function () {

    Manager.LoadMonthDDL();
    Manager.LoadYearDDL();
    Manager.LoadMemberDDL();

    $('#findButton').click(function () {
        var yearId = $('#yearDDL').val();
        var MonthId = $('#monthDDL').val();
        var MemberId = $('#nameDDL').val();

        if (dTable == null) {
            Manager.GetDataForTable(yearId, MonthId, MemberId, 0);
        }
        else {
            Manager.GetDataForTable(yearId, MonthId, MemberId, 1);
        }

    });    

});

var Manager = {

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

    LoadMemberDDL: function () {
        var serviceUrl = '/MemberCost/GetMemberInfo/';
        var jsonParam = '';
        AjaxManager.SendJson(serviceUrl, jsonParam, onSuccess, onFailed);
        function onSuccess(jsonData) {
            AjaxManager.populateCombo('nameDDL', jsonData, 'All');
        }
        function onFailed() {
        }
    },

  
    GetDataForTable: function (yearId, monthId, memberId, refresh) {
        var jsonParam = { yearId: yearId, monthId: monthId, memberId: memberId };
        var serviceURL = "/MemberCost/GetMemberInformation/";
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
                        data: 'MemberName',
                        name: 'MemberName',
                        title: 'Member Name',
                        width: 100
                    },

                    {
                        data: 'TotalDeposit',
                        name: 'TotalDeposit',
                        title: 'Total Deposit',
                        width: 100
                    },

                    {
                        data: 'TotalCost',
                        name: 'TotalCost',
                        title: 'Total Cost',
                        width: 60
                    },

                    {
                        data: 'TotalMeal',
                        name: 'TotalMeal',
                        title: 'Total Meal',
                        width: 60
                    },

                    {
                        data: 'MealRate',
                        name: 'MealRate',
                        title: 'Meal Rate',
                        width: 50
                    },   

                    {
                        data: 'Balance',
                        name: 'Balance',
                        title: 'Balance',
                        width: 80
                    },                      

                ],
                data: data
            });
        } else {
            dTable.clear().rows.add(data).draw();
        }
    },
}
