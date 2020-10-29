var _id = null;
var dTable = null;
var detailTable = null;
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
        var serviceUrl = '/PaymentReport/GetMonthInfo/';
        var jsonParam = '';
        AjaxManager.SendJson(serviceUrl, jsonParam, onSuccess, onFailed);
        function onSuccess(jsonData) {
            AjaxManager.populateCombo('monthDDL', jsonData, "Select Month Name");
        }
        function onFailed() {
        }
    },

    LoadYearDDL: function () {

        var serviceUrl = '/PaymentReport/GetYearInfo/';
        var jsonParam = '';
        AjaxManager.SendJson(serviceUrl, jsonParam, onSuccess, onFailed);
        function onSuccess(jsonData) {
            AjaxManager.populateCombo('yearDDL', jsonData, "Select Year Name");
        }
        function onFailed() {
        }
    },

    LoadMemberDDL: function () {

        var serviceUrl = '/PaymentReport/GetMemberInfo/';
        var jsonParam = '';
        AjaxManager.SendJson(serviceUrl, jsonParam, onSuccess, onFailed);
        function onSuccess(jsonData) {
            AjaxManager.populateCombo('nameDDL', jsonData, 'All');
        }
        function onFailed() {
        }
    },

    //Details
    PaymentDetails: function (yearId, monthId, memberId, refresh) {
            var jsonParam = { yearId: yearId, monthId: monthId, memberId: memberId };
            var serviceURL = "/PaymentReport/GetDetailsInfo/";
            AjaxManager.SendJsonAsyncON(serviceURL, jsonParam, onSuccess, onFailed);
            function onSuccess(jsonData) {

                Manager.LoadDetailsDataTable(jsonData, refresh);
        }

        function onFailed(xhr, status, err) {
            Message.Exception(xhr);
            alert("Failed");
        }
    },

    //Details   
    LoadDetailsDataTable: function (data, refresh) {
            if (refresh == "0") {
            detailTable = $('#detailsTable').DataTable({
                lengthMenu: [[5, 10, 15, 20], [5, 10, 15, 20, "All"]],
                columnDefs: [
                    { visible: false, targets: [] },
                    { className: "dt-center", targets: [0, 1] }
                ],
                searching: false,
                lengthChange: false,
                paging: false,
                info: false,
                //Total Amount
                drawCallback: function () {
                    var sum = $('#detailsTable').DataTable().column(1).data().sum();
                    $('#total').html(sum);
                },
                columns: [
                    {
                        data: 'Date',
                        name: 'Date',
                        title: 'Date',
                        width: 100
                        //width: 100,
                        //render: function (cellValue, type, row) {
                        //    return moment(cellValue).format("DD-MM-YYYY");
                        //}
                    },
                    {
                        data: 'Amount',
                        name: 'Amount',
                        title: 'Total Deposit',
                        width: 100
                    },
                ],
                data: data
            });
    } else {
        detailTable.clear().rows.add(data).draw();
        }
        
    },

    GetDataForTable: function (yearId, monthId, memberId, refresh) {
        var jsonParam = { yearId: yearId, monthId: monthId, memberId: memberId };
        var serviceURL = "/PaymentReport/GetPaymentInformation/";
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
                    { className: "dt-center", targets: [0, 1, 2] }
                ],
                columns: [

                    {
                        data: 'Name',
                        name: 'Name',
                        title: 'Member Name',
                        width: 100
                    },

                    {
                        data: 'Amount',
                        name: 'Amount',
                        title: 'Total Deposit',
                        width: 100
                    },
                    {
                        title: 'Details',
                        width: 50,
                        render: function (data, type, row) {
                            var detailsBtn = '';
                            detailsBtn = '<span class="glyphicon glyphicon-eye-open spnDataTableDetails" title="Details"></span>';

                            return detailsBtn;
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
    },
}

//Details
$(document).on('click', '.spnDataTableDetails', function () {
    var rowData = dTable.row($(this).parents('tr')).data();
    //console.log(rowData);
    $('#nameText').html(rowData.Name);
    var yearId = $("#yearDDL").val();
    var monthId = $("#monthDDL").val();
    var memberId = rowData.MemberId;

    if (detailTable == null) {
        Manager.PaymentDetails(yearId, monthId, memberId, 0);
    }
    else {
        Manager.PaymentDetails(yearId, monthId, memberId, 1);
    }
    
    $('#myModalForDetails').modal('show');
});
