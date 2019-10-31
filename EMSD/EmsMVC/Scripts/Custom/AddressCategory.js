$(document).ready(function () {
    $("#ddlPartial").on("change", function () {
        //alert($("#ddlPartial").val());
        if ($("#ddlPartial").val() == "") {
            $("#partialDiv").addClass("hide");
        }
        else {
            $("#partialDiv").removeClass("hide");
            $.ajax({
                url: '/Employee/PartialResult',
                contentType: "application/json; charset=utf-8",
                type: 'GET',
                dataType: 'html',
                success: function (response) {
                    $("#partialDiv").html(response);
                }
            })
        }
    }); 
});