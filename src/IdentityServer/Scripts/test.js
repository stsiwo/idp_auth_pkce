var $ = require("jquery");

$(document).ready(function () {

    $("#priority-btn").click(function () {

        console.log("update btn is clicked");
        var priority = $("#priority").val();
        var isDone = $("input[name='is-done']:checked").val() === '1';


        $("#priority-list").load("ViewComponents/GetPriorityList", { '_maxPriority': priority, '_isDone': isDone })
    })
});
