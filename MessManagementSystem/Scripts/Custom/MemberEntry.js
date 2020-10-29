var _id = null;
var dTable = null;
$(document).ready(function () {

    Manager.GetDataForTable(0);

});

var Manager = {

    ResetForm: function () {
        _id = null;
        $('#memberEntryForm')[0].reset();
    },

    SaveMemberEntry: function () {
        //Prompt()....Message.
        if (Message.Prompt()) {
            var jsonParam = $('#memberEntryForm').serialize();
            var serviceURL = "/MemberEntry/SaveMemberEntry/";
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
        var serviceURL = "/MemberEntry/GetMemberEntry/";
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
                        data: 'Name',
                        name: 'Name',
                        title: 'Name',
                        width: 120
                    },

                    {
                        data: 'Email',
                        name: 'Email',
                        title: 'Email',
                        width: 100
                    },

                    {
                        data: 'ContactNo',
                        name: 'ContactNo',
                        title: 'Contact No',
                        width: 110
                    },

                    {
                        data: 'Address',
                        name: 'Address',
                        title: 'Address',
                        width: 100
                    },

                    {
                        data: 'Status',
                        name: 'Status',
                        title: 'Status',
                        width: 100,
                        render: function (data, type, row) {
                            return data == true ? "Active" : "Inactive";
                        }
                    },

                ],
                data: data
            });
        } else {
            dTable.clear().rows.add(data).draw();
        }
    },
}

$(document).on('click', '#saveButton', function () {
    Manager.SaveMemberEntry();
});