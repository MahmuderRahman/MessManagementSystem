var AjaxManager = {
    //Save,Edit,Delete
    SendJson: function (serviceUrl, jsonParams, successCalback, errorCallback) {

        $.ajax({
            url: serviceUrl,
            data: jsonParams,
            success: successCalback,
            error: errorCallback
        });

    },

    //Dropdown
    populateCombo: function (container, data, defaultText) {
        var cbmOptions = "<option value=\"0\">" + defaultText + "</option>";
        $.each(data, function () {
            cbmOptions += '<option value=\"' + this.Id + '\">' + this.Name + '</option>';
        });

        $('#' + container).html(cbmOptions);

    },

    // GetDataForTable
    SendJsonAsyncON: function (serviceUrl, jsonParams, successCalback, errorCallback) {

        $.ajax({
            cache: false,
            async: false,
            type: "POST",
            url: serviceUrl,
            data: jsonParams,
            success: successCalback,
            error: errorCallback
        });

    },
}