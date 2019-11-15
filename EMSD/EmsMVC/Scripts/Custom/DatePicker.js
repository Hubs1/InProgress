$(document).ready(function () {
    $('#DOB').datepicker({
        format: 'dd-M-yyyy',//formatting selected date in [01-Nov-2019] format.
        todayHighlight: true,
        autoclose: true
    });
});