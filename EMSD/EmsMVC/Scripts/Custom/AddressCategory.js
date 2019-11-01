$(document).ready(function () {
    
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
        data: { employeeId: $("#EId").val() },
        url: '/Employee/PartialResult',
        contentType: "application/json; charset=utf-8",
        type: 'GET',
        dataType: 'html',
        success: function (response) {
            $("#partialDiv").html(response);
        }
    })
}