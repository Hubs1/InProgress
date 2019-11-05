$(document).ready(function () {
    if ($("#EId").val() > 0) {
        $("#partialDiv").removeClass("hide");
        getEmployeeAdress();
    }

    $("#ddlPartial").on("change", function () {
        if ($("#ddlPartial").val() == "") {
            $("#partialDiv").addClass("hide");
        }
        else {
            $("#partialDiv").removeClass("hide");
            getEmployeeAdress();
        }
    });
});

function getEmployeeAdress() { 
    $.ajax({
        data: { employeeId: $("#EId").val(), addressTypeId: $("#ddlPartial").val() },
        url: '/Employee/PartialResult',
        contentType: "application/json; charset=utf-8",
        type: 'GET',
        dataType: 'html',
        success: function (response) {
            $("#partialDiv").html(response);
        }
    })
}